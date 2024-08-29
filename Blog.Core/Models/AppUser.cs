using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    public string NickName { get; set; }
    public bool IsAdmin { get; set; }
}
