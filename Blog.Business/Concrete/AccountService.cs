using Microsoft.AspNetCore.Identity;
using Blog.Core.Models;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Blog.Business.Absract;
using Blog.Business.Shared.Concrete;
using Blog.Data.Shared.Abstract;
public class AccountService(IRepository<AppUser> appUserRepo) : Service<AppUser>(appUserRepo), IAccountService
{


}
  

