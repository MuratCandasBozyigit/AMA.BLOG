using System;

namespace Blog.Core.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public int? ParentCategoryId { get; set; }  // ParentCategoryId opsiyonel olabilir, bu yüzden nullable yapıldı.



        public int AppUserId { get; set; }
        public virtual AppUser AppUsers { get; set; }
    }
}
