using BotanikBambu.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Route("Admin")]
public class HomeController : AdminBaseController
{
    public HomeController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    [Route("")]
    public IActionResult Index()
    {
        return View();
    }
}
