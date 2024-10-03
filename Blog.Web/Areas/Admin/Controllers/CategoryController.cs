using Blog.Business.Absract;
using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public CategoryController(ICategoryService categoryService, ITagService tagService)
        {
            _categoryService = categoryService;
            _tagService = tagService;
        }

        // GET: Admin/Category
        [HttpGet]
        public IActionResult Index()
        {
            var model = new Category
            {
                Tags = _tagService.GetAll()
            };
            return View(model);
        }

        // GET: Admin/Category/GetAllCategories
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

        // DELETE: Admin/Category/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
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

        // PUT: Admin/Category/Update/{id}
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
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: Admin/Category/GetById/{id}
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID format.");
            }

            try
            {
                var category = _categoryService.GetById(id);
                if (category == null)
                {
                    return NotFound("Category not found.");
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: Admin/Category/Add
        [HttpPost]
        public IActionResult Add([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Invalid category data.");
            }


            try
            {
                // If tags are null or empty, create a new list
                if (category.Tags == null || !category.Tags.Any())
                {
                    category.Tags = new List<Tag>();
                }

                var addedCategory = _categoryService.Add(category);
                return CreatedAtAction(nameof(GetById), new { id = addedCategory.Id }, addedCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
