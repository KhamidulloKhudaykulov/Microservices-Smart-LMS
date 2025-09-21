namespace PostService.Domain.Entities;

public class Post
{
    public Post() { }
    private Post(
        Guid id,
        string title,
        string body,
        Guid userId)
    {
        Id = id;
        Title = title;
        Body = body;
        UserId = userId;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; } = default!;
    public string Body { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }

    public Guid UserId { get; private set; }

    public ICollection<Comment>? Comments { get; private set; }

    public static Result<Post> Create(
        Guid id,
        string title,
        string body,
        Guid userId)
    {
        return new Post(id, title, body, userId);
    }
}
