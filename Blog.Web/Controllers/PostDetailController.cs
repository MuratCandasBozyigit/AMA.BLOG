using Blog.Business.Absract;
using Blog.Core.Models;
using Blog.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class PostDetailController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService; // Yorum servisi eklendi

        public PostDetailController(IPostService postService, ICategoryService categoryService, ICommentService commentService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _commentService = commentService; // Yorum servisi kullanıma alındı
        }

        [HttpGet]
        public async Task<IActionResult> Index(int postId) // async eklendi
        {
            try
            {
                // Post detaylarını al
                var post = await _postService.GetPostDetails(postId);


                if (post == null)
                {
                    return NotFound(); // Post bulunamazsa 404 döndür
                }

                // Post'tan kategori kimliğini al ve kategoriyi getir
                var category = await _categoryService.GetByIdAsync(post.CategoryId); // Asenkron metod çağrıldı
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
                    Comments = comments.ToList(), // IEnumerable'ı listeye çeviriyoruz
                    CategoryName = category.Name // Kategori ismi ViewBag yerine ViewModel üzerinden paylaşıldı
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
            if (ModelState.IsValid)
            {
                await _commentService.AddAsync(comment); // Comment servisini kullanarak ekleme işlemi
                return RedirectToAction("Index", new { postId = comment.PostId }); // Yorum eklendikten sonra post detay sayfasına yönlendirin
            }

            // Eğer model geçersizse, aynı sayfaya geri dön
            return RedirectToAction("Index", new { postId = comment.PostId });
        }
    }
}
