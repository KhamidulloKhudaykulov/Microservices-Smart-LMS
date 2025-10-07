using GradeModule.Domain.Enitites;
using System.Linq.Expressions;

namespace GradeModule.Domain.Repositories;

public interface IGradeRepository
{
    Task<GradeEntity> InsertAsync(GradeEntity entity);
    Task<GradeEntity> UpdateAsync(GradeEntity entity);
    Task DeleteAsync(GradeEntity entity);
    Task<GradeEntity> SelectByIdAsync(Guid id);
    Task<GradeEntity> SelectAsync(Expression<Func<GradeEntity, bool>> predicate);
    Task<IEnumerable<GradeEntity>> SelectAllAsync(Expression<Func<GradeEntity, bool>>? predicate = null);
    IQueryable<GradeEntity> SelectAsQueryable(Expression<Func<GradeEntity, bool>>? predicate = null);
}
