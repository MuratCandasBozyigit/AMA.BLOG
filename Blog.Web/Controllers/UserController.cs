using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
         //Kullanıcı girş yapıp tyorum yapaiblme özelliğine sahip olsun giriş yapamayan kullanıcıya swal ile hata ver
    }
}
