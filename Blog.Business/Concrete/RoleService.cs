using Blog.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<ApplicationRole>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync(); // Tüm rolleri döndür
        }

        public async Task<IdentityResult> CreateRoleAsync(ApplicationRole role)
        {
            return await _roleManager.CreateAsync(role); // Rol oluştur
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId); // Rolü bul
            return await _roleManager.DeleteAsync(role); // Rolü sil
        }

        public async Task<ApplicationRole> GetRoleByIdAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId); // Rolü ID ile bul
        }
    }
}
