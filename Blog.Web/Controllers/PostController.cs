using Microsoft.AspNetCore.Mvc;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using Blog.Data.Shared.Concrete;
using Blog.Business.Absract;

namespace Blog.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postRepository; // Repository'yi kullanacağız

        public PostController(IPostService postRepository)
        {
            _postRepository = postRepository;
        }


        [HttpGet("Index")]
        public IActionResult Index()
        {
            var posts = _postRepository.GetAll();
            return View(posts);
        }

        // GET: Post/Details/5
        public IActionResult Details(int id)
        {
            var post = _postRepository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // GET: Post/Create
        public IActionResult Add()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Post post)
        {
            if (ModelState.IsValid)
            {
                _postRepository.Add(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Post/Edit/5
        public IActionResult Edit(int id)
        {
            var post = _postRepository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _postRepository.Update(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Post/Delete/5
        public IActionResult Delete(int id)
        {
            var post = _postRepository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _postRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
