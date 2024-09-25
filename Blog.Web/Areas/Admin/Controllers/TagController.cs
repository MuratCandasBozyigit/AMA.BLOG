using Blog.Business.Absract;
using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class TagController : Controller
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var tags = tagService.GetAll();
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        //[HttpPost]
        public IActionResult Add([FromBody] Tag tag)
        {
            try
            {
              var tags =tagService.Add(tag);
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Tag tag)
        {
            if (tag == null)
            {
                return BadRequest();
            }

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

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if (id == null)
            {
                return BadRequest("Invalid Id Format");
            }

            try
            {
                var tag = tagService.GetById(id);
                if (tag == null)
                {
                    return NotFound("Tag Not Found");
                }
                return Ok(tag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id ==null)
            {
                return BadRequest("Invalid Id Format");
            }

            var tag = tagService.GetById(id);
            if (tag == null)
            {
                return NotFound("Tag Not Found");
            }

            tagService.Delete(id);
            return Ok();
        }
    }
}
