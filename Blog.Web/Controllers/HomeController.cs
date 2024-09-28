using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        private readonly ICommentService _commentService;

        public HomeController(ILogger<HomeController> logger, IPostService postService, ICategoryService categoryService, ICommentService commentService)
        {
            _logger = logger;
            _postService = postService;
            _categoryService = categoryService;
            _commentService = commentService;
        }

        public class HomeViewModel
        {
            public IEnumerable<Post> Posts { get; set; }
            public IEnumerable<Category> Categories { get; set; }
        }

        // Ana sayfa
        public async Task<IActionResult> Index()
        {
            try
            {
                // Asenkron işlemler
                var posts = await _postService.GetAllPostsAsync(); // Asenkron metod
                var categories = await _categoryService.GetAllCategoriesAsync(); // Asenkron metod

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
                return View("Error");
            }
        }

        // Post detayını getir
        public async Task<IActionResult> Details(int postId)
        {
            try
            {
                // Asenkron metodları çağır
                var post = await _postService.GetAllPostsAsync();
                var categories = await _categoryService.GetAllCategoriesAsync();
                var comments = await _commentService.GetCommentsByPostIdAsync(postId);

                // Verileri ViewBag ile taşı
                ViewBag.Posts = post;
                ViewBag.Categories = categories;
                ViewBag.Comments = comments;

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Detaylar yüklenirken hata oluştu.");
                return BadRequest(ex.Message + " Post yorumu yüklenemedi");
            }
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
