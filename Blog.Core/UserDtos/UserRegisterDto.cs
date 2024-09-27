using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.UserDtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Lütfen Email giriniz")]
        public EmailAddressAttribute Email {  get; set; }
        [Required(ErrorMessage = "Lütfen Adınızı giriniz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen Şifrenizi giriniz")]
        public int Password { get; set; }
    }
}
