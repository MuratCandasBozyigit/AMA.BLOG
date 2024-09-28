using Blog.Business.Absract;
using Blog.Business.Shared.Concrete;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Concrete
{
    public class PostService : Service<Post>, IPostService
    {
        private readonly IRepository<Post> _repo;

        public PostService(IRepository<Post> postRepo) : base(postRepo)
        {
            _repo = postRepo;
        }

        public ICollection<Post> GetAllPosts(Post post)
        {
            return _repo.GetAll(x => x.Id == post.Id).ToList();
        }

        public ICollection<Post> GetAllPostsByCategoryId(int postId)
        {
            return _repo.GetAll(x => x.Id == postId).ToList(); // Belki tag include etmek isteyebilirim daha sonrasında...
        }

        public ICollection<Post> GetAllPosts()
        {
            return _repo.GetAll().ToList(); // Parametresiz versiyon
        }

        // Asenkron versiyonlar
        public async Task<ICollection<Post>> GetAllPostsAsync()
        {
            return await Task.FromResult(_repo.GetAll().ToList());
        }

        public async Task<ICollection<Post>> GetAllPostsByCategoryIdAsync(int categoryId)
        {
            return await Task.FromResult(_repo.GetAll(x => x.CategoryId == categoryId).ToList());
        }
    }
}
