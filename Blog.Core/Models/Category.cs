﻿using System.Collections.Generic;

namespace Blog.Core.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public string Slug { get; set; }
        public int? ParentCategoryId { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }

       

        //public virtual ICollection<Tag> Tag { get; set; } = new List<Tag>(); 

        //public int AppUserId { get; set; }
        //public virtual AppUser AppUser { get; set; }

        // Kategoriye ait postlar
        //public virtual ICollection<Post> Posts { get; set; }
    }
}
