using Blog.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public interface IRoleService
    {
        Task<List<ApplicationRole>> GetAllRolesAsync(); // Tüm rolleri döndür
        Task<IdentityResult> CreateRoleAsync(ApplicationRole role); // Rol oluştur
        Task<IdentityResult> DeleteRoleAsync(string roleId); // Rol sil
        Task<ApplicationRole> GetRoleByIdAsync(string roleId); // Rolü ID ile bul
    }
}
