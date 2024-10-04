using Blog.Core.Models;

public class Comment : BaseModel
{
    public string Content { get; set; }
    public string AuthorId { get; set; } // AppUser ID olarak değiştirin
    public DateTime DateCommented { get; set; }
    public int PostId { get; set; } // Post ile ilişki
    public Post Post { get; set; } // Navigasyon özelliği
}
