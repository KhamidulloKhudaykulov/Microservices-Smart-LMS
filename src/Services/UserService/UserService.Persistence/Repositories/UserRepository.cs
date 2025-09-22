


using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
        _users = context.Set<User>();
    }

    public async Task<User> InsertAsync(User user, CancellationToken cancellationToken)
    {
        await _users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public IQueryable<User> SelectAllAsQueryable(Expression<Func<User, bool>>? predicate = null)
    {
        return predicate is null
            ? _users.AsQueryable()
            : _users.Where(predicate).AsQueryable();
    }

    public async Task<IEnumerable<User>> SelectAllAsync(Expression<Func<User, bool>>? predicate = null)
    {
        return predicate is null
            ? await _users.ToListAsync()
            : await _users.Where(predicate).ToListAsync();
    }

    public async Task<User?> SelectAsync(Expression<Func<User, bool>>? predicate = null)
    {
        return predicate is null
            ? await _users.FirstOrDefaultAsync()
            : await _users.FirstOrDefaultAsync(predicate);
    }
}
