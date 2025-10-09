using HomeworkModule.Domain.Aggregates;
using System.Linq.Expressions;

namespace HomeworkModule.Domain.Repositories;

public interface IHomeworkRepository
{
    Task<Homework> InsertAsync(Homework homework);
    Task<Homework> UpdateAsync(Homework homework);
    Task DeleteAsync(Homework homework);
    Task<Homework?> SelectByIdAsync(Guid courseId, Guid id);
    Task<Homework?> SelectAsync(Guid courseId, Expression<Func<Homework, bool>> predicate);
    Task<IEnumerable<Homework>> SelectAllAsync(Guid courseId, Expression<Func<Homework, bool>>? predication = null);
    IQueryable<Homework> SelectAllAsQueryable(Guid courseId, Expression<Func<Homework, bool>>? predication = null);
}
