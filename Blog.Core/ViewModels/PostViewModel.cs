using Blog.Core.Models; 
using System.Collections.Generic;

namespace Blog.Core.ViewModels
{
    public class PostDetailsViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public string CategoryName { get; set; }


        public Category Category => Post.Category;
    
        public string Title => Post.Title;
        public string Content => Post.Content; 
        public string? AuthorId => Post.AuthorId;
        public string? ImagePath => Post.ImagePath;
        public int? Likes => Post.Likes; 
        public DateTime DatePublished => Post.DatePublished; 
        public bool IsPublished => Post.IsPublished; 
        public string Summary => Post.Summary; 
    }
}
