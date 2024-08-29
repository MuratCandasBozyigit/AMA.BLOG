using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models
{
    public class User:BaseModel
    {

        public string UserName { get; set; }
        //Passwordu Hash Yap gizilee
        public string Password { get; set; }
        public string Email { get; set; }
        public int DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ProfilePictureUrl { get; set; }
        public bool IsAdmin { get; set; }   

    }
}
