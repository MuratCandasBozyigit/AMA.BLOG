using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models
{
    public class Tag:BaseModel
    {
       

        public string Name {  get; set; }
        public int? PostId { get; set; }
        public string TagDescription { get; set; }
        //Slug(URL dostu etiket adı)



      //  Etiketleri category de birden fazla secebilmek icin
      //  public virtual ICollection<Category> Category { get; set; }

        //public int? AppUserId { get; set; }
        //public virtual AppUser AppUser { get; set; }
    }
}
