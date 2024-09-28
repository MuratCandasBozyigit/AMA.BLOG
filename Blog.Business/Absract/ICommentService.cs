using Blog.Business.Shared.Absract;
using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Blog.Business.Absract
{
    public interface ICommentService:IService<Comment>
    {
        Task<ICollection<Comment>> GetCommentsByPostIdAsync(int postId);
        ICollection<Comment> GetCommentsByPostId(int postId);
    }
}
