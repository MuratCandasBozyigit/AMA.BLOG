using Blog.Business.Absract;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class PostDetailController : Controller
    {
        private readonly IPostService _postService;

        public PostDetailController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        ////Post detayını getir
        //public IActionResult Details() { return View(); }
    }
}
