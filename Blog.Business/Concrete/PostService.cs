using Blog.Business.Absract;
using Blog.Business.Shared.Concrete;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Concrete
{
    public class PostService : Service<Post>, IPostService
    {
        private readonly IRepository<Post> _postRepo;
        private readonly IRepository<Category> _categoryRepo; 
        private readonly IRepository<Comment> _commentRepo; 

        public PostService(IRepository<Post> postRepo, IRepository<Category> categoryRepo, IRepository<Comment> commentRepo) : base(postRepo)
        {
            _postRepo = postRepo;
            _categoryRepo = categoryRepo;
            _commentRepo = commentRepo;
        }

        public ICollection<Post> GetAllPostsByCategoryId(int categoryId)
        {
            return _postRepo.GetAll(x => x.CategoryId == categoryId).ToList();
        }

        public ICollection<Post> GetAllPosts()
        {
            return _postRepo.GetAll()
                .Include(p=>p.Category)
                .ThenInclude(t=>t.Tag)
                .Include(p=>p.Comments)
                .ToList();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _postRepo.GetByIdAsync(id);
        }

        public async Task<Post> GetPostDetails(int postId)
        {
            var post = await _postRepo.GetByIdAsync(postId);
            if (post == null)
            {
                return null;
            }

          
            post.Category = await _categoryRepo.GetByIdAsync(post.CategoryId);

         
            post.Comments = (await _commentRepo.GetCommentsByPostIdAsync(postId)).ToList(); 
            return post;
        }

        public ICollection<Post> GetAllPosts(Post post)
        {
            return _postRepo.GetAll().Include(p=>p.Category).ToList();
        }
    }
}
