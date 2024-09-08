using Blog.Business.Absract;
using Blog.Business.Concrete;
using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var tag = tagService.GetAll();
                return Ok(tag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Tag tag)
        {
            try
            {
                tagService.Add(tag);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Update/{id}")]
        public IActionResult Update([FromBody] Tag tag)
        {
            if (tag == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    tagService.Update(tag);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Id Format");
            }
            try
            {
                var tag = tagService.GetById(id);
                if (tag == null)
                {
                    return NotFound("Category Not Found");
                }
                return Ok(tag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Id Format");
            }

            var tag = tagService.GetById(id);
            if (tag == null)
            {
                return NotFound("Category Not Found");
            }

            tagService.Delete(id);
            return Ok();
        }

    }

}
