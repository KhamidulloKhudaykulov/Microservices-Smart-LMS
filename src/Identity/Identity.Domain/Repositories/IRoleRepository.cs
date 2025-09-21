using Identity.Domain.Entities;

public interface IRoleRepository
{
    Task<Role?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Role?> SelectByNameAsync(string name, CancellationToken cancellationToken = default);
    Task InsertAsync(Role role, CancellationToken cancellationToken = default);
    Task UpdateAsync(Role role, CancellationToken cancellationToken = default);
}