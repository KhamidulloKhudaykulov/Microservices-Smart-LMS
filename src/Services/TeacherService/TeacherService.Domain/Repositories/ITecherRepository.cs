using System.Linq.Expressions;
using TeacherService.Domain.Aggregates;

namespace TeacherService.Domain.Repositories;

public interface ITecherRepository
{
    Task<Teacher> InsertAsync(Teacher teacher);
    Task<Teacher> UpdateAsync(Teacher teacher);
    Task<Teacher> DeleteAsync(Teacher teacher);
    Task<Teacher?> SelectByIdAsync(Guid id);
    Task<Teacher?> SelectAsync(Expression<Func<Teacher, bool>> predicate);
    Task<IEnumerable<Teacher>> SelectAllAsync(Expression<Func<Teacher, bool>>? predicate = null);
    IQueryable<Teacher> SelectAsQueryable(Expression<Func<Teacher, bool>>? predicate = null);
}
