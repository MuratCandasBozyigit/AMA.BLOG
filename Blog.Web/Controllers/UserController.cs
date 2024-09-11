using Microsoft.AspNetCore.Mvc;
using Blog.Business.Absract;
using Blog.Dtos.UserDTOs;


namespace Blog.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            LoginDTO dto = await _userService.Login(loginDTO);



            if (dto.Role is not null)
                return RedirectToAction("Index", "Home", new { area = dto.Role });
            else
                return RedirectToAction("Login");




        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Login");
        }

    }
}