using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
