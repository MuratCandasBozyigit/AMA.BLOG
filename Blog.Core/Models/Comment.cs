using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blog.Core.Models
{
    public class Comment:BaseModel
    {
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public DateTime DateCommented { get; set; }

    }
}
