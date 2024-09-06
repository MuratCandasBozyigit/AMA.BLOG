using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Blog.Business.Absract;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

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

        // Index action
        public async Task<IActionResult> Index()
        {
            try
            {
                var posts =  _postService.GetAllPosts(); // Tüm postları al
                return View(posts); // View'a gönder
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ana sayfa yüklenirken hata oluştu.");
                return View("Error"); // Hata sayfasını döndür
            }
        }

        // All posts retrieval
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var posts = _postService.GetAllPosts(); // Tüm postları al
                return Json(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Postlar yüklenirken hata oluştu.");
                return BadRequest("Postlar yüklenemedi, lütfen tekrar deneyin.");
            }
        }

        // Post by ID retrieval
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Geçersiz konu ID.");
            }
            try
            {
                var post = _postService.GetById(id);
                if (post == null)
                {
                    return NotFound("Konu bulunamadı.");
                }
                return Json(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} ile konu yüklenirken hata oluştu.");
                return BadRequest("Konu yüklenemedi, lütfen tekrar deneyin.");
            }
        }
        // Error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}