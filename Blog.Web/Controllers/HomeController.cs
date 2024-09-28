using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic; // IEnumerable için gerekli
using System.Diagnostics;
using System.Threading.Tasks;
using Blog.Core.Models;
using Blog.Business.Absract;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        public HomeController(ILogger<HomeController> logger, IPostService postService, ICategoryService categoryService, ITagService tagService)
        {
            _logger = logger;
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
        }


        //Etiketleride listeleyebilrsin istersen tagı getirdim.
        public async Task<IActionResult> Index()
        {
            try
            {
                var posts = _postService.GetAllPosts(); // Senkron şekilde postları al
                var categories = _categoryService.GetAllCategories(); // Senkron şekilde kategorileri al
                var tags = _tagService.GetAll();
                var model = new HomeViewModel
                {
                    Posts = posts,
                    Categories = categories,
                    Tags = tags
                };

                return View(model); // View'a model ile birlikte gönder
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ana sayfa yüklenirken hata oluştu.");
                return View("Error"); // Hata sayfasını döndür
            }
        }

        public IActionResult PostDetails(int id)
        {
            var post = _postService.GetById(id); 
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        public void AddCommentToPost(int postId, Comment comment)
        {
            var post = _postService.GetById(postId);
            if (post != null)
            {
                // Post'un yorum koleksiyonu null değilse doğrudan ekle
                if (post.Comment == null)
                {
                    post.Comment = new List<Comment>();
                }

                post.Comment.Add(comment); // Yorumları eklemek için
                _postService.Update(post);
            }
        }

        //[HttpPost]
        //public IActionResult AddComment(int postId, string commentContent)
        //{
        //    // Kullanıcının ID'sini al
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    // Yeni yorumu oluştur
        //    var comment = new Comment
        //    {
        //        Content = commentContent,
        //        AuthorId = userId, // Kullanıcının kimliği buraya atanıyor
        //        DateCommented = DateTime.Now
        //    };

        //    _postService.AddCommentToPost(postId, comment); // Servis üzerinden yorumu ekle
        //    return RedirectToAction("PostDetails", new { id = postId });
        //}

        [HttpPost]
        public IActionResult UpdateComment(int postId, int commentId, string commentContent)
        {
            // Kullanıcının ID'sini al
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Güncellenmiş yorumu oluştur
            var updatedComment = new Comment
            {
                Id = commentId,
                Content = commentContent,
                AuthorId = userId, // Gerekli ise burada da kullanabilirsin
                DateCommented = DateTime.Now
            };

            _postService.UpdateCommentInPost(postId, updatedComment); // Servis üzerinden yorumu güncelle
            return RedirectToAction("PostDetails", new { id = postId });
        }



        //public async Task<IActionResult> GetPostContent(Post post)
        //{
        //    try
        //    {
        //        var posts = _postService.GetAllPosts(); // Senkron şekilde postları al
        //        var categories = _categoryService.GetAllCategories(); // Senkron şekilde kategorileri al
        //        var tags = _tagService.GetAll();

        //        var model = new ContentViewModel
        //        {
        //            Posts = posts,
        //            Categories = categories,
        //            Tags = tags

        //        };
        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Post içeriği yüklenirken hata oluştu.");
        //        return View("Error"); // Hata sayfasını döndür
        //    };
        //}

        public class ContentViewModel
        {
            public IEnumerable<Post> Posts { get; set; }
            public IEnumerable<Category> Categories { get; set; }
            public IEnumerable<Tag> Tags { get; set; }
        }

        public class HomeViewModel
        {
            public IEnumerable<Post> Posts { get; set; }
            public IEnumerable<Category> Categories { get; set; }
            public IEnumerable<Tag> Tags { get; set; }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

    }
}
