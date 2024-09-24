using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DTOS.UserDTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Düz metin şifre (şu anda)
        public bool IsAdmin { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}
