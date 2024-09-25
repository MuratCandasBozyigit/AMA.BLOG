using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models
{
    public class PostEditViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }

}
