using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;

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

        public HomeController(ILogger<HomeController> logger, IPostService postService, ICategoryService categoryService)
        {
            _logger = logger;
            _postService = postService;
            _categoryService = categoryService;
        }



        public async Task<IActionResult> Index()
        {
            try
            {
                var posts = _postService.GetAllPosts(); // Senkron şekilde postları al
                var categories = _categoryService.GetAllCategories(); // Senkron şekilde kategorileri al

                var model = new HomeViewModel
                {
                    Posts = posts,
                    Categories = categories
                };

                return View(model); // View'a model ile birlikte gönder
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ana sayfa yüklenirken hata oluştu.");
                return View("Error"); // Hata sayfasını döndür
            }
        }

        //PostDetaişlsViewinde kullanacagım.
        //[HttpGet]
        //public IActionResult Details(int postId)
        //{
        //    try
        //    {
        //        // Post detaylarını al
        //        var post = _postService.GetPostDetails(postId); // Doğrudan Post nesnesini al

        //        if (post == null)
        //        {
        //            return NotFound(); // Post bulunamazsa 404 döndür

        //        }
        //        return View(post);
        //    } // Tekil postu görünüm ile paylaş

        //    catch (Exception ex)
        //    {
        //        ViewBag.ErrorMessage = "Post yüklenirken bir hata oluştu. Lütfen tekrar deneyin.";
        //        return View("Error");
        //    }
        //}

        public class HomeViewModel
        {
            public IEnumerable<Post> Posts { get; set; }
            public IEnumerable<Category> Categories { get; set; }
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
