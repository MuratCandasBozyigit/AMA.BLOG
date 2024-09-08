using System.Threading.Tasks;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using Blog.Web.Models;
using Microsoft.AspNetCore.Identity;

public interface IAccountService
{
    Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
    Task<SignInResult> LoginUserAsync(LoginViewModel model);
    Task LogoutUserAsync();
}
