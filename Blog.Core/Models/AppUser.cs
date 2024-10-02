using Blog.Core.Models;
using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    
    public string FullName { get; set; }
 
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public string? ProfilePictureUrl { get; set; }

    public bool IsAdmin { get; set; } = false;
  
}



//Gereksiz zaten hesap acılmadan okunulabilinyor ?D

////Yaşa göre içerik olabilir diye yaş aldım bunun aşagıda calışmaya başladıktan sonra kodu ayarla hesap yapsın kullanıcı 18 den veya 13 ten kücükse statement ile hallet home ındexte gözükecek itemler ona göre listelensin boş ise ındex uygun içerik yok gibi bir yazılı sayfaya yada içerik henüz yok giib bir sayfaya yonlendrilsin 
//public int DateOfBirth { get; set; }   //public string Password { get; set; }
//public string Email { get; set; } //Passwordu Hash Yap gizilee