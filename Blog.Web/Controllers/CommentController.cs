using Blog.Business.Absract;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public CommentController(ICommentService commentService, IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }

      
        public async Task<IActionResult> Index(int postId)
        {
            var post = await _postService.GetByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            var comments = await _commentService.GetComments(postId);
            return View(comments);
        }

       
        [HttpGet]
        public IActionResult AddComment(int postId)
        {
            var model = new Comment { PostId = postId };
            return View(model);
        }

      
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.DateCommented = DateTime.Now; 
                await _commentService.AddAsync(comment);
                return RedirectToAction("Index", new { postId = comment.PostId });
            }
            return View(comment);
        }

       
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int postId)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment != null)
            {
                await _commentService.DeleteAsync(comment);
            }
            return RedirectToAction("Index", new { postId });
        }
    }
}
