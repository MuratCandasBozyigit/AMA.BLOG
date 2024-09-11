using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Models;
namespace Blog.Models.Dtos
{
    public class UserDto : BaseModel
    {
        public bool IsAdmin { get; set; } = false;
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? Description { get; set; }
        public string UserName { get; set; }
        public string ApplicationForm { get; set; }


        // public DateTime? BirthDate { get; set; }
        // public string? ProfilePicture { get; set; }
        // public string? Instagram { get; set; }
        // public string? Facebook { get; set; }
        // public string? Twitter { get; set; }


    }
}
