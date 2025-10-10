using SharedKernel.Domain.Primitives;
using System.Linq.Expressions;

namespace ScheduleModule.Domain.Repositories;

public interface IScheduleRepository<T>
    where T : Entity
{
    Task<T> InsertAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T?> SelectByIdAsync(Guid id);
    Task<T?> SelectAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> SelectAllAsync(Expression<Func<T, bool>>? predicate = null);
    IQueryable<T> SelectAllAsQueryable(Expression<Func<T, bool>>? predicate = null);
}
