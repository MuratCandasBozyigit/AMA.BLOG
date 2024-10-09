using System.Collections.Generic;

namespace Blog.Core.Models
{
    public class Post : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? AuthorId { get; set; }
        public string? ImagePath { get; set; }
        public int? Likes { get; set; }

        public DateTime DatePublished { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } = true;
        public string Summary { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

       public string TagName { get; set; }
        public Tag Tag { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>(); 
    }
}
