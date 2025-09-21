using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Role> _roles;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
        _roles = _context.Set<Role>();
    }

    public async Task<Role?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _roles
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<Role?> SelectByNameAsync(string name, CancellationToken cancellationToken = default)
        => await _roles
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Name == name, cancellationToken);

    public async Task InsertAsync(Role role, CancellationToken cancellationToken = default)
    {
        await _roles.AddAsync(role, cancellationToken);
    }

    public async Task UpdateAsync(Role role, CancellationToken cancellationToken = default)
    {
        await Task.FromResult(_roles.Update(role));
    }
}
