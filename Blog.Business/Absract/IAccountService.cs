using System.Threading.Tasks;
using Blog.Business.Shared.Absract;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using Blog.Web.Models;
using Microsoft.AspNetCore.Identity;

public interface IAccountService : IService<AppUser>
{
}
