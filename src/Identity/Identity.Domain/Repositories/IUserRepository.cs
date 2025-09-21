using Identity.Domain.Entities;

namespace Identity.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> SelectByIdAsync(Guid id, CancellationToken cancellationToken = default);
    IQueryable<User?> SelectByIdAsQueryable(Guid id, CancellationToken cancellationToken = default);
    Task<User?> SelectByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task InsertAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
}