using CourseModule.Domain.Entitites;

namespace CourseModule.Domain.Repositories;

public interface ICourseRepository
{
    Task<CourseEntity> InsertAsync(CourseEntity entity);
    Task<CourseEntity> UpdateAsync(CourseEntity entity);
    Task DeleteAsync(CourseEntity entity);
    Task<CourseEntity?> SelectByIdAsync(Guid id);
    Task<IEnumerable<CourseEntity>> SelectAllByAccountIdAsync(Guid accountId);
    Task<CourseEntity?> SelectByNameAsync(string name);
    Task<IEnumerable<CourseEntity>> SelectAllAsync();
    IQueryable<CourseEntity> SelectAllAsQueryable();
}
