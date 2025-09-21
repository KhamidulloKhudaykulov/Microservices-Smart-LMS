using CommentService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Entities;

namespace PostService.Persistence.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Comment> _comments;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
        _comments = _context.Set<Comment>();
    }

    public async Task DeleteAsync(Comment comment)
    {
        await Task.FromResult(_comments.Remove(comment));
    }

    public async Task<Comment> InsertAsync(Comment comment)
    {
        return (await _comments.AddAsync(comment)).Entity;
    }

    public IQueryable<Comment> SelectAllAsQueryable()
    {
        return _comments
            .AsNoTracking()
            .AsQueryable();
    }

    public async Task<IEnumerable<Comment>> SelectAllAsync()
    {
        return await Task.FromResult(_comments.AsEnumerable());
    }

    public async Task<Comment?> SelectByIdAsync(Guid id)
    {
        return await _comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        return await Task.FromResult(_comments.Update(comment).Entity);
    }
}
