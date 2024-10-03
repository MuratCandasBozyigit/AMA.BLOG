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


        public  List<ApplicationRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList(); // Tüm rolleri döndür
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
        public async Task<IdentityResult> UpdateRoleAsync(ApplicationRole role)
        {
            var existingRole = await _roleManager.FindByIdAsync(role.Id);

            if (existingRole != null)
            {
                existingRole.Name = role.Name;
                existingRole.NormalizedName = role.NormalizedName;
                existingRole.Description = role.Description; // ApplicationRole modeline göre

                return await _roleManager.UpdateAsync(existingRole);
            }
            return IdentityResult.Failed(new IdentityError { Description = "Rol bulunamadı." });
        }

        public async Task<ApplicationRole> GetRoleByIdAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId); // Rolü ID ile bul
        }
    }
}
