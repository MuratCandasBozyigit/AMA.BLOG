using Microsoft.AspNetCore.Mvc;
using Blog.Business.Absract;
using Blog.Dtos.UserDTOs;
using Blog.Business.Concrete;


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


        #region GirişCıkış 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDTO);
            }

            var user = _userService.GetByEmail(loginDTO.Email);

            if (user == null ||  _userService.GetPassword(user, loginDTO.Password))
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                return View(loginDTO);
            }

            // Login başarılı -> oturum açılır
            // Session, Cookie veya Authentication mekanizmaları burada kullanılabilir
            HttpContext.Session.SetString("UserId", user.Id.ToString());

            return RedirectToAction("Index", "Home");
        }
        // Login başarılı -> oturum açılır
        // Session, Cookie veya Authentication mekanizmaları burada kullanılabilir
     
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
        public IActionResult GetByEmail(string email)
        {
            if (email == null)
            {
                return BadRequest("Email bulunamadı");
            }
            else
            {
                try
                {
                    var mail = _userService.GetByEmail(email);
                    return Ok(mail);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message + "mayil bülunamadi");
                }
            }
        }

        public IActionResult GetPassword(string password)
        {
            if (password == null)
            {
                return BadRequest("Şifre boş kankss");
            }
            else
            {
                try { var pwd = _userService.GetPassword(password); return Ok(pwd); } catch (Exception ex) { return BadRequest(ex.Message + "şifrede sorin vardir gardaş"); }
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
            if (user != null)
            {
                return BadRequest("Kullanıcı Mevcut");
            }
            else
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
        }
        #endregion
    }
}