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

        // Yorumları getir
        public async Task<IEnumerable<Comment>> GetComments(int postId)
        {
            return await _context.Set<Comment>()
                                 .Where(c => c.PostId == postId) // PostId ile filtreleme
                                 .ToListAsync(); // Liste olarak döndür
        }

        public async Task<IQueryable<Comment>> GetCommentsById(int id)
        {
            return _context.Set<Comment>().Where(c => c.Id == id); // IQueryable döndür
        }
        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Set<Comment>().FirstOrDefaultAsync(c => c.Id == id);
        }
        // Yorum ekleme metodu
        public async Task AddAsync(Comment comment)
        {
            await _context.Set<Comment>().AddAsync(comment); // Yorum ekleme işlemi
            await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydet
        }

        // Yorum silme metodu
        //public async Task DeleteAsync(int id)
        //{
        //    var comment = await _context.Set<Comment>().FindAsync(id); // Silinecek yorumu bul
        //    if (comment != null)
        //    {
        //        _context.Set<Comment>().Remove(comment); // Yorum sil
        //        await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydet
        //    }
        //}
        public async Task DeleteAsync(Comment comment)
        {
            if (comment != null)
            {
                _context.Set<Comment>().Remove(comment); // Yorum sil
                await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydet
            }
        }
    }
}
