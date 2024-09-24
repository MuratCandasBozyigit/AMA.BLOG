﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Models;
namespace Blog.Dtos.UserDTOs
{
    public class LoginDTO:AppUser
    {
      
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
            public bool IsRememberMe { get; set; }
            public string? Role { get; set; }
       
    }

}