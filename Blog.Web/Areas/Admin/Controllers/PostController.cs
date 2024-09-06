using Microsoft.AspNetCore.Mvc;
using Blog.Core.Models;

using Blog.Business.Absract;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        public PostController(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }


        [HttpGet("Post/Index")]
        public IActionResult Index()
        {
            return View();

        }
        #region OTHER_CRUDS

        public IActionResult GetAllCategories(Category category)
        {
            if (category == null)
            {
                BadRequest("Kategori bulunamadı");
                return View();
            }
            else
            {
                try
                {
                    var cat = _categoryService.GetAllCategories(category);
                    return Json(category);
                }
                catch (Exception ex)
                {
                    return BadRequest("İlgili kategori bulunamadı");
                }

            }
        }
        #endregion

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Add(Post post, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                // Resmin ismini ve yolunu oluştur
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                // Resmi belirlenen klasöre kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                // Resmin yolunu veritabanına kaydet
                post.ImagePath = "~/images/" + fileName; // Örnek: /images/filename.jpg
            }

            // Diğer post bilgilerini kaydet
            _postService.Add(post);
            return RedirectToAction("Index");
        }


        // GET: Post/Details/5
        [HttpGet("Post/Details/{id}")]
        public IActionResult Details(int id)
        {
            var post = _postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        public IActionResult GetAll()
        {
            var posts = _postService.GetAll();
            return View(posts);
        }



        //  [ValidateAntiForgeryToken]
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest("Post cannot be null");
            }

            try
            {
                _postService.Add(post); // Veritabanına ekleme işlemi
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); // Replace with proper logging
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // GET: Post/Update/5
        [HttpGet("Post/Update/{id}")]
        public IActionResult Update(int id)
        {
            var post = _postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/Update/5
        [HttpPost("Post/Update/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _postService.Update(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost("Post/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var post = _postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            _postService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
