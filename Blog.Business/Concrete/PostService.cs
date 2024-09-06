using Blog.Business.Absract;
using Blog.Business.Shared.Concrete;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Concrete
{
    public class PostService(IRepository<Post> postRepo) : Service<Post>(postRepo), IPostService
    {
        private readonly IRepository<Post> _repo = postRepo;
        public ICollection<Post> GetAllPosts(Post post)
        {
            return _repo.GetAll(x=> x.Id == post.Id).ToList();
           
        }

        public ICollection<Post> GetAllPostsByCategoryId(int postId)
        {
            return _repo.GetAll(x => x.Id == postId).ToList();//Belki tag include etmek isteyebilirim daha sonrasında...
        }
        public ICollection<Post> GetAllPosts()
        {
            return _repo.GetAll().ToList(); // Parametresiz versiyon
        }


    }
}
