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

        // POST: Admin/Category/Add
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Category cannot be null");
                }

                _categoryService.Add(category);
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


        [HttpPut("Update")]
        public IActionResult Update([FromBody] Category category)
        {
            if (category == null || category.Id == 0)
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


        //// DELETE: Admin/Category/Delete/{id}
        //[HttpDelete("Delete")]
        //public IActionResult Delete([FromQuery] Guid id)
        //{
        //    if (id == Guid.Empty)
        //    {
        //        return BadRequest("Invalid ID format.");
        //    }

        //    var color = _categoryService.GetFirstOrDefault(i => i.GuidId == id);
        //    if (color == null)
        //    {
        //        return NotFound("Color not found.");
        //    }

        //    _categoryService.Delete(color.Id);
        //    return Ok(color);
        //}-


    }
}
