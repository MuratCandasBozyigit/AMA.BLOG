using Blog.Business.Absract;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class PostDetailController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService categoryService;
        public PostDetailController(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index(int postId)
        {
            try
            {
                // Post detaylarını al
                var post = _postService.GetPostDetails(postId); // Doğrudan Post nesnesini al

                if (post == null)
                {
                    return NotFound(); // Post bulunamazsa 404 döndür
                }

                return View(post); // Tekil postu görünüm ile paylaş
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Post yüklenirken bir hata oluştu. Lütfen tekrar deneyin.";
                return View("Error");
            }
        }








        //[HttpGet]
        //public IActionResult Index(int postId)
        //{
        //    try
        //    {
        //        var post = _postService.GetPostDetails(postId);
        //        return View(post);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.ErrorMessage = "Post yüklenirken bir hata oluştu. Lütfen tekrar deneyin.";
        //        return View("Error");
        //    }
        //}
        ////Post detayını getir
        //public IActionResult Details() { return View(); }
    }
}
