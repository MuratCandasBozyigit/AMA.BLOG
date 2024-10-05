using Blog.Business.Absract;
using Blog.Core.Models;
using Blog.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    // [Authorize] 
    public class PostDetailController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService; // Yorum servisi
        private readonly UserManager<AppUser> _userManager; // Kullanıcı yöneticisi

        public PostDetailController(IPostService postService, ICategoryService categoryService, ICommentService commentService, UserManager<AppUser> userManager)
        {
            _postService = postService;
            _categoryService = categoryService;
            _commentService = commentService;
            _userManager = userManager; // Kullanıcı yöneticisini kullanıma al
        }

        [HttpGet]
        public async Task<IActionResult> Index(int postId)
        {
            try
            {
                // Post detaylarını al
                var post = await _postService.GetPostDetails(postId);

                if (post == null)
                {
                    return NotFound(); // Post bulunamazsa 404 döndür
                }

                // Kategoriyi al
                var category = await _categoryService.GetByIdAsync(post.CategoryId);
                if (category == null)
                {
                    return NotFound("Kategori bulunamadı."); // Kategori bulunamazsa 404 döndür
                }

                // Yorumları al
                var comments = await _commentService.GetCommentsByPostIdAsync(postId);

                // Görünümde kullanmak için modeli hazırlayın
                var viewModel = new PostDetailsViewModel
                {
                    Post = post,
                    Comments = comments.ToList(),
                    CategoryName = category.Name
                };

                return View(viewModel); // Tekil postu ve yorumları görünüm ile paylaş
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Post yüklenirken bir hata oluştu. Lütfen tekrar deneyin.";
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Yorum yapabilmek için giriş yapmalısınız." });
            }

          
                comment.DateCommented = DateTime.Now;
                comment.AuthorId = _userManager.GetUserId(User);

                var user = await _userManager.FindByIdAsync(comment.AuthorId);
                if (user != null)
                {
                    comment.Author = user; 
                }

                comment.PostId = comment.PostId;

                await _commentService.AddAsync(comment);

                return Json(new
                {
                    success = true,
                    author = comment.Author?.FullName ?? "Anonim", 
                    dateCommented = comment.DateCommented.ToShortDateString(),
                    content = comment.Content
                });
           

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return Json(new
            {
                success = false,
                message = "Geçersiz yorum.",
                errors = errors.Select(e => e.ErrorMessage)
            });
        }


    }
}
