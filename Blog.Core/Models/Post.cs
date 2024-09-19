using Blog.Core.Models;
namespace Blog.Core.Models
{
    public class Post : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? AuthorId { get; set; }
        //İmage pathı şimidlik nullable yaptım düzenle bunu sonrasında normal kalsın hata dondursun
        public string? ImagePath { get; set; }
        public int? Likes { get; set; }
        public int? Comments { get; set; }
        public string Summary { get; set; }

        public DateTime DatePublished { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } =true;

        //public ICollection<Tag> Tags { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


        
    }
}