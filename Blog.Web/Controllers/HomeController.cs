using Blog.Business.Absract;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        public HomeController(ILogger<HomeController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllPosts(Post post)
        {
            if (post == null)
            {
                return BadRequest("Gönderi yok");
            }
            else
            {
                try
                {
                    var posts = _postService.GetAllPosts(post);
                    return Json(posts);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message + "Ppost bulunamadı ex mesdsage");
                }
            }
           
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
