namespace PostService.Domain.Entities;

public class Comment
{
    public Comment() { }
    private Comment(Guid id, string content, Guid userId, Guid postId)
    {
        Id = id;
        Content = content;
        UserId = userId;
        PostId = postId;
    }

    public Guid Id { get; private set; }
    public string Content { get; private set; } = default!;

    public Guid UserId { get; private set; }

    public Guid PostId { get; private set; }
    public Post? Post { get; private set; }

    public static Result<Comment> Create(
        Guid id,
        string content,
        Guid userId,
        Guid postId)
    {
        return new Comment(id, content, userId, postId);
    }
}
