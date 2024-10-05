using Blog.Core.Models;

public class Comment : BaseModel
{
    public string? Content { get; set; }
    public string AuthorId { get; set; } 
    public AppUser Author { get; set; } 
    public DateTime DateCommented { get; set; } = DateTime.Now;

    public int PostId { get; set; } 
    public Post Post { get; set; } 
}
