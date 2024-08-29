using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models
{
    public class BaseModel
    {

        public int Id { get; set; }
        public Guid GuidId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
       
    }
}
