using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Blog.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? ProfilePictureUrl { get; set; }

        public bool IsAdmin { get; set; } = false;

      
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
    }
}
