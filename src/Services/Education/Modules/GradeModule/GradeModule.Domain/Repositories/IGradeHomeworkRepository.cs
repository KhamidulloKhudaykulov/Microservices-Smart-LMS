using GradeModule.Domain.Enitites;
using System.Linq.Expressions;

namespace GradeModule.Domain.Repositories;

public interface IGradeHomeworkRepository
{
    Task<GradeHomeworkEntity> InsertAsync(GradeHomeworkEntity entity);
    Task<GradeHomeworkEntity> UpdateAsync(GradeHomeworkEntity entity);
    Task DeleteAsync(GradeHomeworkEntity entity);
    Task<GradeHomeworkEntity?> SelectByIdAsync(Guid id);
    Task<GradeHomeworkEntity?> SelectAsync(Expression<Func<GradeHomeworkEntity, bool>> predicate);
    Task<IEnumerable<GradeHomeworkEntity>> SelectAllAsync(Expression<Func<GradeHomeworkEntity, bool>>? predicate = null);
    IQueryable<GradeHomeworkEntity> SelectAsQueryable(Expression<Func<GradeHomeworkEntity, bool>>? predicate = null);
}
