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

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
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
}
