using Blog.Business.Absract;
using Blog.Business.Concrete;
using Blog.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #region Tamamlandı 

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetAllCategories")]
        public IActionResult GetAll()
        {
            try
            {
                var categories = _categoryService.GetAll();
                return Json(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid ID format.");
            }

            var category = _categoryService.GetFirstOrDefault(i => i.Id == id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            _categoryService.Delete(category.Id);
            return Ok(category);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            if (category == null || category.Id != id)
            {
                return BadRequest("Invalid category data.");
            }

            try
            {
                _categoryService.Update(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id Format");
            }
            try
            {
                var category = _categoryService.GetById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Category category)
        {
            try
            {
                var addedCategory = _categoryService.Add(category);
                return Ok(addedCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal category server error: {ex.Message}");
            }
        }

        #endregion
    }
}
