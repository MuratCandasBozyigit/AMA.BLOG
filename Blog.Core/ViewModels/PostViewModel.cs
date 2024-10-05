using Blog.Core.Models; // Post ve Comment sınıflarını kullanmak için gerekli
using System.Collections.Generic;

namespace Blog.Core.ViewModels
{
    public class PostDetailsViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public string CategoryName { get; set; }


        public Category Category => Post.Category;
        // Post nesnesinden doğrudan erişim için
        public string Title => Post.Title; // Başlık
        public string Content => Post.Content; // İçerik
        public string? AuthorId => Post.AuthorId; // Nullable AuthorId
        public string? ImagePath => Post.ImagePath; // Nullable ImagePath
        public int? Likes => Post.Likes; // Nullable Likes
        public DateTime DatePublished => Post.DatePublished; // Yayın Tarihi
        public bool IsPublished => Post.IsPublished; // Durum
        public string Summary => Post.Summary; // Özet
    }
}
