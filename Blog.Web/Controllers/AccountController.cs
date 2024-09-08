using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Blog.Core.Models;
using Blog.Business.Absract;


namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IAppUserService _appUserService;
        //Ekstradan servis eklememek için appdbcontedxten halledicem
        private readonly ApplicationDbContext _context;

        public AccountController(IAccountService accountService, ApplicationDbContext context, IAppUserService appUserService)
        {
            _accountService = accountService;
            _context = context;
            _appUserService = appUserService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUser user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }

            AppUser appUser = _context.AppUser.FirstOrDefault(u => u.Id == user.Id && u.UserName == user.UserName);
            if (appUser == null)
            {
                return Unauthorized(); // Daha uygun HTTP yanıtı
            }

            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
        new Claim(ClaimTypes.GivenName, appUser.UserName),
        new Claim(ClaimTypes.Role, appUser.IsAdmin ? "Admin" : "User")
    };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties
            {
                IsPersistent = true
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Register(AppUser appUser)
        {
            if (appUser == null)
            {
                var userApp = _appUserService.Add(appUser);
                return Ok(appUser);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}