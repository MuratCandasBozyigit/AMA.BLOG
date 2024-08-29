using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models
{
    public class Category:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //Admin parent category ekleyebilir gerekli olmayada bilir kücük bir sistem sonuçta henüz
        public int ParentCategoryId {  get; set; } 
        //Slug(URL dostu kategori adı)
      
    }
}
