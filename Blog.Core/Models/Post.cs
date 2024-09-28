using Blog.Core.Models;
using System;
using System.Collections.Generic;

namespace Blog.Core.Models
{
    public class Post : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? AuthorId { get; set; }

        // ImagePath'ı nullable yaptınız, bunu isterseniz sonrasında zorunlu hale getirebilirsiniz.
        public string? ImagePath { get; set; }

        public int? Likes { get; set; }
        public int? Comments { get; set; }
        public string Summary { get; set; }

        public DateTime DatePublished { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } = true;

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Comment koleksiyonu başlatıldı
        public virtual ICollection<Comment> Comment { get; set; } = new List<Comment>();
    }
}
