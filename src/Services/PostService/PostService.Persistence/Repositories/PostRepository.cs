using Microsoft.EntityFrameworkCore;
using PostService.Domain.Entities;
using PostService.Domain.Repositories;

namespace PostService.Persistence.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Post> _posts;

    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
        _posts = _context.Set<Post>();
    }

    public async Task DeleteAsync(Post post)
    {
        await Task.FromResult(_posts.Remove(post));
    }

    public async Task<Post> InsertAsync(Post post)
    {
        return (await _posts.AddAsync(post)).Entity;
    }

    public IQueryable<Post> SelectAllAsQueryable()
    {
        return _posts
            .AsNoTracking()
            .AsQueryable();
    }

    public async Task<IEnumerable<Post>> SelectAllAsync()
    {
        return await Task.FromResult(_posts.AsEnumerable());
    }

    public async Task<Post?> SelectByIdAsync(Guid id)
    {
        return await _posts.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        return await Task.FromResult(_posts.Update(post).Entity);
    }
}
