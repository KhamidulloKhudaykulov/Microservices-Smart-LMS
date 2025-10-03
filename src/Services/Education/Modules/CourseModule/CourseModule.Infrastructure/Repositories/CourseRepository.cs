using CourseModule.Domain.Entitites;
using CourseModule.Domain.Repositories;
using CourseModule.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CourseModule.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly CourseDbContext _dbContext;
    private readonly DbSet<CourseEntity> _courses;

    public CourseRepository(CourseDbContext dbContext)
    {
        _dbContext = dbContext;
        _courses = _dbContext.Set<CourseEntity>();
    }

    public Task DeleteAsync(CourseEntity entity)
    {
        _courses.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<CourseEntity> InsertAsync(CourseEntity entity)
    {
        return (await _courses.AddAsync(entity)).Entity;
    }

    public IQueryable<CourseEntity> SelectAllAsQueryable()
    {
        return _courses.AsQueryable();
    }

    public async Task<IEnumerable<CourseEntity>> SelectAllAsync()
    {
        return await _courses.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<CourseEntity>> SelectAllByAccountIdAsync(Guid accountId)
    {
        return await _courses
            .AsNoTracking()
            .Where(c => c.AccountId == accountId)
            .ToListAsync();
    }

    public async Task<CourseEntity?> SelectByIdAsync(Guid id)
    {
        return await _courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<CourseEntity?> SelectByNameAsync(string name)
    {
        return await _courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name);
    }

    public Task<CourseEntity> UpdateAsync(CourseEntity entity)
    {
        _courses.Update(entity);
        return Task.FromResult(entity);
    }
}
