using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Blog.Core.Models
{
    public class ApplicationRole : IdentityRole
    {
        // Role ile ilgili ek bilgiler
        public string RoleName { get; set; }
        public string Description { get; set; }

        // Bu rolün hangi kullanıcılarla ilişkili olduğunu tutar
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
    }
}
