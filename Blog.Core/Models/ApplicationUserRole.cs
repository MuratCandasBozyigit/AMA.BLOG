using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models
{
    public class ApplicationUserRole
    {
        // Kullanıcı ID'si ve ilişki
        public string UserId { get; set; }
        public AppUser User { get; set; }

        // Rol ID'si ve ilişki
        public string RoleId { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
