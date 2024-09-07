using Blog.Core.Models;
using Microsoft.AspNetCore.Identity;

public class AppUser : BaseModel
{
    public string UserName { get; set; }
    public int? AppUserId { get; set; }
    //Passwordu Hash Yap gizilee
    public string Password { get; set; }
    public string Email { get; set; }


    public int DateOfBirth { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public string ProfilePictureUrl { get; set; }

    public bool IsAdmin { get; set; }


   
 

}
