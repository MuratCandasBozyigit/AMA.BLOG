using Blog.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Blog.Core.Models; 

public interface IAppUserService
{
    Task<AppUser> GetUserByIdAsync(string userId);
    Task<AppUser> GetUserByEmailAsync(string email);
    Task<IdentityResult> CreateUserAsync(AppUser user, string password);
    Task<SignInResult> SignInUserAsync(string email, string password, bool rememberMe);
    Task SignOutUserAsync();
}
