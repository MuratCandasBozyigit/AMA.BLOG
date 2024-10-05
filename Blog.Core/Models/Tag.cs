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
      




      //  public virtual ICollection<Category> Category { get; set; }

        //public int? AppUserId { get; set; }
        //public virtual AppUser AppUser { get; set; }
    }
}
