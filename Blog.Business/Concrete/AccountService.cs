using Microsoft.AspNetCore.Identity;
using Blog.Core.Models;
using Blog.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
    {
        var user = new AppUser { UserName = model.Email, Email = model.Email };
        return await _userManager.CreateAsync(user, model.Password);
    }

    public async Task<SignInResult> LoginUserAsync(LoginViewModel model)
    {
        return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
    }

    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
