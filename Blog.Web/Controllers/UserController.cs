using Microsoft.AspNetCore.Mvc;
using Blog.Business.Absract;
using Blog.Dtos.UserDTOs;
using Blog.Business.Concrete;
using Blog.DTOS.UserDTOs;


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
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            // Kullanıcıyı email ile veritabanından buluyoruz
            var user = await _userService.GetByEmailAsync(loginDto.Email);

            // Kullanıcı null ise ya da şifre yanlışsa hata mesajı döndür
            if (user == null || !await _userService.CheckPasswordAsync(user, loginDto.Password))
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                return View(loginDto);
            }

            // Başarılı giriş durumunda oturum açma işlemleri
            HttpContext.Session.SetString("UserId", user.Id.ToString());

            return RedirectToAction("Index", "Home");
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
        public IActionResult Add([FromBody] RegisterDTO registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest("Kullanıcı bilgileri eksik.");
            }

            try
            {
                // Kullanıcının var olup olmadığını kontrol ediyoruz
                var existingUser = _userService.GetByEmail(registerDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("Bu email ile zaten bir kullanıcı mevcut.");
                }

                // DTO'dan AppUser'a dönüşüm
                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    Password = registerDto.Password,
                    IsAdmin = registerDto.IsAdmin = false,
                    //Bu alttaki ücü sorun cıkarıyor ona göre bunların değerlerinde de sorun var db yede ekleyemiyorum istedğim kadar migrate etsemde convert etmek icin alttakileri getirdim hata gider gibi düşnüp null eyledim ama olmadi 
                    DateOfBirth = registerDto.DateOfBirth = DateTime.Today,
                    ProfilePicture = null,
                    ApplicationForm = null,
                };

                var addedUser = _userService.Add(appUser);
                return Ok(addedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        #endregion
    }
}


//public IActionResult Add([FromBody] AppUser user)
//{
//    if (user != null)
//    {
//        return BadRequest("Kullanıcı Mevcut");
//    }
//    else
//    {
//        try
//        {
//            var appUser = _userService.Add(user);
//            return Ok(appUser);
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }
//}