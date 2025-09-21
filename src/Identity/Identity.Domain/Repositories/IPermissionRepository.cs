using Identity.Domain.Entities;

public interface IPermissionRepository
{
    Task<Permission?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Permission?> SelectByNameAsync(string name, CancellationToken cancellationToken = default);
    Task InsertAsync(Permission permission, CancellationToken cancellationToken = default);
}
