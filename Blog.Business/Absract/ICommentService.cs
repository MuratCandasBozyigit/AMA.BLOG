using Blog.Business.Shared.Absract;
using Blog.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Business.Absract
{
    public interface ICommentService : IService<Comment>
    {
    
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<Comment> GetByIdAsync(int id);
        Task<IEnumerable<Comment>> GetComments(int postId);
        Task<IQueryable<Comment>> GetCommentsById(int id);

  
        Task AddAsync(Comment comment);

       
        // Task DeleteAsync(int id);

        Task DeleteAsync(Comment comment);
    }
}
