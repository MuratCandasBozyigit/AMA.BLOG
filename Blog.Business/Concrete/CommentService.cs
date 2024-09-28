using Blog.Business.Absract;
using Blog.Business.Shared.Concrete;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using System.Collections.Generic;

namespace Blog.Business.Concrete
{
    public class CommentService : Service<Comment>, ICommentService
    {
        private readonly IRepository<Comment> _commentRepo;

        // Yapıcı metot: commentRepo'yu dışarıdan alıp yerel alan (_commentRepo) ile ilişkilendiriyoruz
        public CommentService(IRepository<Comment> commentRepo) : base(commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public ICollection<Comment> GetCommentsByPostId(int postId)
        {
            throw new NotImplementedException();
        }

        // Post ID'ye göre yorumları getir
        //public ICollection<Comment> GetCommentsByPostId(int postId)
        //{
        //    var comments = _commentRepo.GetByCondition(c => c.PostId == postId); // PostId'ye göre filtreleme yapıyoruz
        //    return comments ?? new List<Comment>(); // Eğer null ise boş liste döndür
        //}
        public async Task<ICollection<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await Task.FromResult(_commentRepo.GetAll(x => x.PostId == postId).ToList());
        }

    }
}
