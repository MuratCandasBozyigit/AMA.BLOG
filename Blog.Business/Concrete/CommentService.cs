using Blog.Business.Absract;
using Blog.Business.Shared.Concrete;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using Microsoft.EntityFrameworkCore; // Bu yönergeyi ekleyin
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Concrete
{
    public class CommentService : Service<Comment>, ICommentService // Hatalı sözdizimi düzeltildi
    {
        private readonly ApplicationDbContext _context;

        public CommentService(IRepository<Comment> commentRepo, ApplicationDbContext context) : base(commentRepo)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetComments(int postId)
        {
            return await _context.Set<Comment>()
                                 .Where(c => c.PostId == postId) // Burada PostId kullanıyoruz
                                 .ToListAsync(); // Liste olarak döndür
        }


        public async Task<IQueryable<Comment>> GetCommentsById(int id)
        {
            return _context.Set<Comment>().Where(c => c.Id == id); // IQueryable döndür
        }
    }
}
