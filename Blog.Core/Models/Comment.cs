using Blog.Core.Models;

public class Comment : BaseModel
{
    public string Content { get; set; }
    public string AuthorId { get; set; } // AppUser ID olarak değiştirin
    public DateTime DateCommented { get; set; }
}
