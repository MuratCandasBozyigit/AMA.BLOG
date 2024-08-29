using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models
{
    public class Post:BaseModel
    {


        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public DateTime DatePublished { get; set; }
        public bool IsPublished { get; set; }
        public string Tags { get; set; }

        //SLUG EKLE 
    }
}
