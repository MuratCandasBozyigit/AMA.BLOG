using Blog.Core.Models;

public class Post : BaseModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string AuthorId { get; set; }
    public DateTime DatePublished { get; set; }
    public bool IsPublished { get; set; }
    public ICollection<Tag> Tags { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
