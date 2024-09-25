using Microsoft.AspNetCore.Mvc;
using Blog.Core.Models;
using System.IO;
using System;
using Blog.Business.Absract;
using Blog.Business.Concrete;
using Microsoft.Extensions.Hosting;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class PostController : Controller
    {
        #region Servisler 
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public PostController(IPostService postService, ICategoryService categoryService, ITagService tagService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        #endregion
        // GET: Post/Index
        [HttpGet("Index")]
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

        #region Tamamlandı 

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            _postService.GetAll();
            return Ok();
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            var categories = _categoryService.GetAll(); // Kategorileri al
            var viewModel = new HomeViewModel
            {
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromForm] Post post, IFormFile image)
        {

            if (image != null && image.Length > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                post.ImagePath = "/images/" + fileName;
            }

            _postService.Add(post);
            return Json(new { success = true, message = "Post başarıyla kaydedildi." });



        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) // null kontrolü yerine 0 ile kontrol
            {
                return BadRequest("Invalid Id Format");
            }

            var post = _postService.GetById(id);
            if (post == null)
            {
                return NotFound("Post Not Found");
            }

            _postService.Delete(id);
            return Ok();
        }

        [HttpGet("GetById{id}")]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Geçerli ıd yok ");
            }
            try
            {
                var Post = _postService.GetById(id);
                return Ok(Post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "sea sorun getbyıd");
            }
        }

        #endregion


        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var post = _postService.GetById(id);
            if (post == null)
            {
                return NotFound();
            }

            var categories = _categoryService.GetAll();
            var viewModel = new PostEditViewModel
            {
                Post = post,
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost("Edit/{id}")]
        public IActionResult Edit(PostEditViewModel viewModel, IFormFile image)
        {
            var postToUpdate = _postService.GetById(viewModel.Post.Id);
            if (postToUpdate == null)
            {
                return NotFound();
            }

            postToUpdate.Title = viewModel.Post.Title;
            postToUpdate.Content = viewModel.Post.Content;
            postToUpdate.CategoryId = viewModel.Post.CategoryId;

            if (image != null && image.Length > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                postToUpdate.ImagePath = "/images/" + fileName;
            }

            _postService.Update(postToUpdate);
            return Json(new { success = true, message = "Post başarıyla güncellendi." });
        }
    }
}







#region YORUMSATIRLARISSSS

//ASIL EDİT BU
//[HttpGet]
//public IActionResult Edit()
//{
//    return View();
//}
//[HttpPost]
//public IActionResult Edit([FromBody] Post post)
//{
//    if (post == null)
//    {
//        return BadRequest("Post nesnesi null.");
//    }

//    if (!ModelState.IsValid)
//    {
//        return BadRequest("Post model doğrulama hatası.");
//    }

//    try
//    {
//        // Eğer 'GetAll' metodunun doğru bir kullanımı değilse, uygun metodu çağırmalısınız.
//        var posts = _postService.GetAll(); // Eğer 'post' ile filtreleme yapılacaksa uygun metodu çağırmalısınız
//        return Ok(posts);
//    }
//    catch (Exception ex)
//    {
//        // Özel hata mesajları veya loglama
//        return StatusCode(500, $"Sunucu hatası: {ex.Message}");
//    }
//}


//[HttpGet]
//public IActionResult Edit()
//{
//    return View();
//}
//[HttpPost]
//public IActionResult Edit([FromBody] Post post)
//{
//    if (post == null)
//    {
//        return BadRequest("Post nesnesi null.");
//    }

//    if (!ModelState.IsValid)
//    {
//        return BadRequest("Post model doğrulama hatası.");
//    }

//    try
//    {
//        // Eğer 'GetAll' metodunun doğru bir kullanımı değilse, uygun metodu çağırmalısınız.
//        var posts = _postService.GetAll(); // Eğer 'post' ile filtreleme yapılacaksa uygun metodu çağırmalısınız
//        return Ok(posts);
//    }
//    catch (Exception ex)
//    {
//        // Özel hata mesajları veya loglama
//        return StatusCode(500, $"Sunucu hatası: {ex.Message}");
//    }
//}



//public IActionResult Create(Post post, IFormFile image)
//{
//    if (ModelState.IsValid)
//    {
//        if (image != null && image.Length > 0)
//        {
//            var fileName = Path.GetFileName(image.FileName);
//            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

//            using (var stream = new FileStream(filePath, FileMode.Create))
//            {
//                image.CopyTo(stream);
//            }

//            post.ImagePath = "~/images/" + fileName;
//        }

//        _postService.Add(post);
//        return RedirectToAction("Index");
//    }

//    return View(post);
//}









//[HttpGet("{id}")]
//public IActionResult Edit(int id)
//{
//    var post = _postService.GetById(id);
//    if (post == null)
//    {
//        return NotFound();
//    }

//    var categories = _categoryService.GetAll();
//    var viewModel = new PostEditViewModel
//    {
//        Post = post,
//        Categories = categories
//    };

//    return View(viewModel);
//}

//[HttpPost]
//public IActionResult Edit(PostEditViewModel viewModel, IFormFile image)
//{
//    if (!ModelState.IsValid)
//    {
//        viewModel.Categories = _categoryService.GetAll(); // Kategorileri yeniden yükle
//        return View(viewModel);
//    }

//    if (image != null && image.Length > 0)
//    {
//        var fileName = Path.GetFileName(image.FileName);
//        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

//        using (var stream = new FileStream(filePath, FileMode.Create))
//        {
//            image.CopyTo(stream);
//        }

//        viewModel.Post.ImagePath = "/images/" + fileName;
//    }

//    _postService.Update(viewModel.Post); // Update metodunu çağırdığınızdan emin olun
//    return RedirectToAction("Index");
//}

#endregion