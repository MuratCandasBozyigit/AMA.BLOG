using System;
using Microsoft.AspNetCore.Identity;

namespace Blog.Core.Models
{
    public class Comment : BaseModel
    {
        public string Content { get; set; }

        // Identity kullanıcı sınıfıyla ilişkilendirilmiş Author
        public string AuthorId { get; set; }
        public AppUser Author { get; set; } // AppUser sınıfı ile ilişkilendirilmiş

        public DateTime DateCommented { get; set; } = DateTime.Now; // Varsayılan olarak yorumun yapıldığı tarih

        // İlgili Post ile ilişkilendirme
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
