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
    public class CommentService : Service<Comment>, ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(IRepository<Comment> commentRepo, ApplicationDbContext context) : base(commentRepo)
        {
            _context = context;
        }

    
        public async Task<IEnumerable<Comment>> GetComments(int postId)
        {
            return await _context.Set<Comment>()
                                 .Where(c => c.PostId == postId) 
                                 .ToListAsync();
        }

        public async Task<IQueryable<Comment>> GetCommentsById(int id)
        {
            return _context.Set<Comment>().Where(c => c.Id == id); 
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Set<Comment>().FirstOrDefaultAsync(c => c.Id == id);
        }

     
        public async Task AddAsync(Comment comment)
        {
            await _context.Set<Comment>().AddAsync(comment);
            await _context.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.Author)
                .ToListAsync();
        }

      
        public async Task DeleteAsync(Comment comment)
        {
            if (comment != null)
            {
                _context.Set<Comment>().Remove(comment); 
                await _context.SaveChangesAsync();
            }
        }
    }
}
