using Microsoft.AspNetCore.Identity;

namespace Blog.Core.Models
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsAdmin { get; set; }
       // public bool IsModerator { get; set; }
        public string Description { get; set; }
    }
}
