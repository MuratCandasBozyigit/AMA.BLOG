﻿using Blog.Business.Absract;
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
        private readonly IRepository<Category> _categoryRepo; // Kategori repository'si
        private readonly IRepository<Comment> _commentRepo; // Yorum repository'si

        public PostService(IRepository<Post> postRepo, IRepository<Category> categoryRepo, IRepository<Comment> commentRepo) : base(postRepo)
        {
            _postRepo = postRepo;
            _categoryRepo = categoryRepo;
            _commentRepo = commentRepo;
        }

        public ICollection<Post> GetAllPostsByCategoryId(int categoryId)
        {
            return _postRepo.GetAll(x => x.CategoryId == categoryId).ToList(); // Kategoriye göre postları al
        }

        public ICollection<Post> GetAllPosts()
        {
            return _postRepo.GetAll().ToList(); // Parametresiz versiyon
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

            // Kategoriyi yükle
            post.Category = await _categoryRepo.GetByIdAsync(post.CategoryId);

            // Yorumları yükle
            post.Comments = (await _commentRepo.GetCommentsByPostIdAsync(postId)).ToList(); // Yorumları listeye çevir

            return post;
        }

        public ICollection<Post> GetAllPosts(Post post)
        {
            return _postRepo.GetAll().ToList();
        }
    }
}
