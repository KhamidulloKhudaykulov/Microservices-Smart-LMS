using System.Linq.Expressions;
using UserService.Domain.Entities;

namespace UserService.Domain.Repositories;

public interface IUserRepository
{
    Task<User> InsertAsync(User user, CancellationToken cancellationToken);
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<User> DeleteAsync(User user, CancellationToken cancellationToken);
    Task<User> SelectAsync(Expression<Func<User, bool>>? predicate = null);
    Task<IEnumerable<User>> SelectAllAsync(Expression<Func<User, bool>>? predicate = null);
    IQueryable<User> SelectAllAsQueryable(Expression<Func<User, bool>>? predicate = null);
}
