using PostService.Domain.Entities;

namespace PostService.Domain.Repositories;

public interface IPostRepository
{
    Task<Post> InsertAsync(Post post);
    Task<Post> UpdateAsync(Post post);
    Task DeleteAsync(Post post);
    Task<Post?> SelectByIdAsync(Guid id);
    Task<IEnumerable<Post>> SelectAllAsync();
    IQueryable<Post> SelectAllAsQueryable();
}
