using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Core.Models;
using Blog.Core.Services;
using System.Threading.Tasks;
using Blog.Business.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class UserListController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRoleService _roleService;
        private readonly UserManager<AppUser> _userManager; // Use your AppUser if you are using it

        public UserListController(IRoleService roleService, UserManager<AppUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _roleService = roleService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch list of users from UserManager
            var users = await _userManager.Users.ToListAsync();
            return View(users); // Pass the list of users to the view
        }

        #region GetALLRoles 
       
        [HttpGet("GetAllRolesAsync")]
        public  IActionResult GetAllRolesAsync()
        {
           var roles =  _roleManager.Roles.ToList();
            return Json(roles);
        }
        #endregion

        #region GetRoleById
        [HttpGet]
        public async Task<IActionResult> GetRoleById(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
                return BadRequest("Role ID cannot be null or empty");

            var role = await _roleService.GetRoleByIdAsync(roleId);

            if (role == null)
                return NotFound("Role not found");

            return Json(role);
        }
        #endregion

        #region Update 
        [HttpPut("UpdateRoleAsync")]
        public async Task<IActionResult> UpdateRoleAsync(string roleId, [FromBody] ApplicationRole model)
        {
            if (roleId == null || model == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                // ID ile rolü bul
                var existingRole = await _roleService.GetRoleByIdAsync(roleId);

                if (existingRole == null)
                {
                    return NotFound("Role not found.");
                }

                // Rol bilgilerini güncelle
                existingRole.RoleName = model.RoleName;
                existingRole.Description = model.Description;
                existingRole.Name = model.RoleName; // Name property is optional
                existingRole.NormalizedName = model.RoleName.ToUpper();

                // Güncelleme işlemi için servis çağır
                var result = await _roleService.UpdateRoleAsync(existingRole);

                if (result.Succeeded)
                {
                    return Ok("Role updated successfully.");
                }

                return BadRequest("Failed to update the role.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region CreateRole 
        [HttpGet("CreateRole")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] ApplicationRole model)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole
                {
                    RoleName = model.RoleName,   // Custom role name
                    Description = model.Description,
                    Name = model.RoleName,       // Setting Name from RoleName
                    NormalizedName = model.RoleName.ToUpper() // NormalizedName in uppercase
                };

                var result = await _roleService.CreateRoleAsync(role);

                if (result.Succeeded)
                {
                    return Ok("Role created successfully.");
                }

                return BadRequest("Failed to create role.");
            }

            return BadRequest("Invalid data.");
        }
        #endregion

        #region DeleteRole 
        [HttpPost("DeleteAsyncRoles")]
        public async Task<IActionResult> DeleteAsyncRoles([FromBody]string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return BadRequest("Role ID cannot be null or empty.");
            }

            var result = await _roleService.DeleteRoleAsync(roleId);

            if (result.Succeeded)
            {
                return Ok("Role deleted successfully.");
            }

            return BadRequest("Failed to delete role.");
        }
        #endregion
    }
}
