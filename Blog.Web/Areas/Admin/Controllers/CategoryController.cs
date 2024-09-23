using Blog.Business.Absract;
using Blog.Business.Concrete;
using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #region Tamamlandı 
        // GET: Admin/Category
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Category/GetAllCategories
        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
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
            if (id == null)
            {
                return BadRequest("Invalid ID format.");
            }

            var iD = _categoryService.GetFirstOrDefault(i => i.Id == id);
            if (iD == null)
            {
                return NotFound("Color not found.");
            }

            _categoryService.Delete(iD.Id);
            return Ok(iD);
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
            if (id == 0)
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

        #endregion


        [HttpPost("Add")]
        public IActionResult Add([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Invalid category data.");
            }

            try
            {
                _categoryService.Add(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal category server error: {ex.Message}");
            }
        }





    }
}
