using HomeworkModule.Domain.Entities;
using System.Linq.Expressions;

namespace HomeworkModule.Domain.Repositories;

public interface IHomeworkRepository
{
    Task<Homework> InsertAsync(Homework homework);
    Task<Homework> UpdateAsync(Homework homework);
    Task DeleteAsync(Homework homework);
    Task<Homework?> SelectByIdAsync(Guid id);
    Task<Homework?> SelectAsync(Expression<Func<Homework, bool>> predicate);
    Task<IEnumerable<Homework>> SelectAllAsync(Expression<Func<Homework, bool>>? predication = null);
    IQueryable<Homework> SelectAllAsQueryable(Expression<Func<Homework, bool>>? predication = null);
}
