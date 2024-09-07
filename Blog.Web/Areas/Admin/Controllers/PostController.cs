using Microsoft.AspNetCore.Mvc;
using Blog.Core.Models;
using System.IO;
using System;
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

        // GET: Post/Index
        [HttpGet]
        public IActionResult Index()
        {
            var posts = _postService.GetAll();
            var categories = _categoryService.GetAll();

            // Modeli oluştur
            var viewModel = new HomeViewModel
            {
                Posts = posts,
                Categories = categories
            };

            // View'e modeli gönder
            return View(viewModel);
        }


        public class HomeViewModel
        {
            public IEnumerable<Post> Posts { get; set; }
            public IEnumerable<Category> Categories { get; set; }
        }



        #region POST_CRUD
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
          _postService.GetAll();
            return Ok();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }

                    post.ImagePath = "~/images/" + fileName;
                }

                _postService.Add(post);
                return RedirectToAction("Index");
            }

            return View(post);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest("Post nesnesi null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Post model doğrulama hatası.");
            }

            try
            {
                // Eğer 'GetAll' metodunun doğru bir kullanımı değilse, uygun metodu çağırmalısınız.
                var posts = _postService.GetAll(); // Eğer 'post' ile filtreleme yapılacaksa uygun metodu çağırmalısınız
                return Ok(posts);
            }
            catch (Exception ex)
            {
                // Özel hata mesajları veya loglama
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }



        #endregion

    }
}
