using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }

    public async Task<User?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(u => u.UserId == id, cancellationToken);

    public async Task<User?> SelectByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => await _users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(u => u.UserName == username, cancellationToken);

    public async Task InsertAsync(User user, CancellationToken cancellationToken = default)
    {
        await _users.AddAsync(user, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        await Task.FromResult(_users.Update(user));
    }

    public IQueryable<User?> SelectByIdAsQueryable(Guid id, CancellationToken cancellationToken = default)
    {
        return _users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .Where(u => u.UserId == id);
    }
}
