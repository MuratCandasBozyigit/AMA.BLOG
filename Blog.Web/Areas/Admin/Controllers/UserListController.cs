using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Areas.Admin.Controllers
{
    public class UserListController : Controller
    {
        //private readonly UserManager<IdentityUser> _userManager;

        //public UserListController(UserManager<IdentityUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        //public async Task<IActionResult> Index()
        //{
        //    var users = await _userManager.Users.ToListAsync();
        //    return View(users);
        //}
        public IActionResult Index()
        {
            return View();
        }
    }
}
