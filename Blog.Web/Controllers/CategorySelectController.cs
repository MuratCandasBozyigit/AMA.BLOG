using Blog.Business.Absract;
using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    public class CategorySelectController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;
        public CategorySelectController(ICategoryService categoryService, IPostService postService)
        {
            this.categoryService = categoryService;
            this.postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllCategories(Category category)
        {
            if (category == null)
            {
                return BadRequest("Kategori yok");
            }
            else
            {
                try
                {
                    categoryService.GetAllCategories(category);
                    return Json(category);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message+"Kategoriyokexmessage");
                }
            }
        }

        

        #region GetPostsByCategory


        //public IActionResult GetAllPostsByCategoryId()
        //{
        //    return Ok(postService.GetAllPostsByCategoryId(int.Parse(User.FindAll(ClaimTypes.NameIdentifier))));
        //}

        private int  GetSelectedCategoryId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        private Category GetCategoryById(int ıd)
        {
            return categoryService.GetFirstOrDefault(x=>x.Id==ıd);
        }

        #endregion
    }
}
