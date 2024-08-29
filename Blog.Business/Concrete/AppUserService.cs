using Blog.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;


public class AppUserService : IAppUserService
{
   
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Diğer metodlar
    


    public async Task<AppUser> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<AppUser> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<SignInResult> SignInUserAsync(string email, string password, bool rememberMe)
    {
        return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
    }

    public async Task SignOutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
