using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    //Ekstradan servis eklememek için appdbcontedxten halledicem
    private readonly ApplicationDbContext _context;

    public AccountController(IAccountService accountService, ApplicationDbContext context)
    {
        _accountService = accountService;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(AppUser user)
    {
        AppUser appUser = _context.AppUser.FirstOrDefault(u => u.Id == user.Id && u.UserName == user.UserName);
        if (appUser == null)
        {
            return Content("Error");
        }
        else
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.GivenName, appUser.UserName));
            claims.Add(new Claim(ClaimTypes.Role, appUser.IsAdmin ? "Admin" : "User"));

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties
            {
                IsPersistent = true
            });
            return RedirectToAction("Index", "Home");
        }

    }


    //Burayı düzenle eve gitmek için erkenden kalktım ardından ViewPageleri ayarla 8 eylülde logini yapmak zorundasın kg 
    [HttpPost]
    public IActionResult Register(AppUser appUser)
    {
        if (appUser == null)
        {
            var appUserAdd = _accountService.Add(appUser);
            return Json(appUserAdd);
        }
        else
        {
            return RedirectToAction("Login", "Account");

            //return BadRequestResult("Bu bilgilerle bir hesap mevcut", RedirectToAction("Login", "Account"));
          
        }
    }
}
