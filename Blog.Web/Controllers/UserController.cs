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
        #region GirişCıkış 
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult GetById(int id)
        {
            if (id == 0)
            { return BadRequest("AYDİ YOK LOGİN "); }
            else
            {
                try
                {
                    var user = _userService.GetById(id);
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }





        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Login");
        }
        #endregion
        #region KayıtOl 
        public IActionResult Register()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "-1";

            return View();

        }

        public IActionResult Add([FromBody] AppUser user)
        {
            try
            {
                var appUser = _userService.Add(user);
                return Ok(appUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}