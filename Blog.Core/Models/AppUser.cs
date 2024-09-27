using Blog.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class AppUser : IdentityDbContext
{
    public string UserName { get; set; }
    public int? AppUserId { get; set; }
    //Passwordu Hash Yap gizilee
    public string Password { get; set; }
    public string Email { get; set; } 
  
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public string? ProfilePictureUrl { get; set; }

    public bool IsAdmin { get; set; }
}

//Gereksiz zaten hesap acılmadan okunulabilinyor ?D

////Yaşa göre içerik olabilir diye yaş aldım bunun aşagıda calışmaya başladıktan sonra kodu ayarla hesap yapsın kullanıcı 18 den veya 13 ten kücükse statement ile hallet home ındexte gözükecek itemler ona göre listelensin boş ise ındex uygun içerik yok gibi bir yazılı sayfaya yada içerik henüz yok giib bir sayfaya yonlendrilsin 
//public int DateOfBirth { get; set; }