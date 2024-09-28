using Blog.Business.Shared.Absract;
using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Absract
{
    public interface IPostService:IService<Post>
    {
        Task<ICollection<Post>> GetAllPostsAsync();
        ICollection<Post> GetAllPosts();

       
        ICollection<Post> GetAllPosts(Post post);
        ICollection<Post> GetAllPostsByCategoryId(int postId);
       
        //Task<ICollection<Post>> GetAllPostsAsync(Post post); 
        Task<ICollection<Post>> GetAllPostsByCategoryIdAsync(int categoryId);
    }
}
