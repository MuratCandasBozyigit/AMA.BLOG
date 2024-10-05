using Blog.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public interface IRoleService
    {
       List<ApplicationRole> GetAllRoles(); 
        Task<IdentityResult> CreateRoleAsync(ApplicationRole role);
        Task<IdentityResult> DeleteRoleAsync(string roleId); 
        Task<ApplicationRole> GetRoleByIdAsync(string roleId); 
        Task<IdentityResult> UpdateRoleAsync(ApplicationRole role);

    }
}
