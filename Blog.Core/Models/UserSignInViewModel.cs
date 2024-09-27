using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage = "Kullanıcı mailini girin lütfen ")]
        public String email { get; set; }
        [Required(ErrorMessage = "Şifre girin lütfen ")]
        public String password { get; set; }
    }
}
