using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Models;

namespace Blog.Core.UserDtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage ="Lütfen Email giriniz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen Şifre giriniz")]
        public int Password { get; set; }
    }
}
