using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class PermissionRepository : IPermissionRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Permission> _permissions;

    public PermissionRepository(ApplicationDbContext context)
    {
        _context = context;
        _permissions = _context.Set<Permission>();
    }

    public async Task<Permission?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _permissions
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<Permission?> SelectByNameAsync(string name, CancellationToken cancellationToken = default)
        => await _permissions
            .FirstOrDefaultAsync(p => p.Name == name, cancellationToken);

    public async Task InsertAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        await _permissions.AddAsync(permission, cancellationToken);
    }
}