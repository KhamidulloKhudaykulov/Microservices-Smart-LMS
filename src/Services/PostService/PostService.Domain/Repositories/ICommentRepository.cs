using PostService.Domain.Entities;

namespace CommentService.Domain.Repositories;

public interface ICommentRepository
{
    Task<Comment> InsertAsync(Comment comment);
    Task<Comment> UpdateAsync(Comment comment);
    Task DeleteAsync(Comment comment);
    Task<Comment> SelectByIdAsync(Guid id);
    Task<IEnumerable<Comment>> SelectAllAsync();
    IQueryable<Comment> SelectAllAsQueryable();
}
