using HomeworkModule.Domain.Aggregates;
using HomeworkModule.Domain.Repositories;
using HomeworkModule.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HomeworkModule.Infrastructure.Repositories;

public class HomeworkRepository : IHomeworkRepository
{
    private readonly HomeworkDbContext _dbContext;
    private readonly DbSet<Homework> _homeworks;

    public HomeworkRepository(HomeworkDbContext dbContext)
    {
        _dbContext = dbContext;
        _homeworks = _dbContext.Set<Homework>();
    }

    public async Task<Homework> InsertAsync(Homework homework)
    {
        await _homeworks.AddAsync(homework);
        return homework;
    }

    public Task<Homework> UpdateAsync(Homework homework)
    {
        _homeworks.Update(homework);
        return Task.FromResult(homework);
    }

    public async Task DeleteAsync(Homework homework)
    {
        _homeworks.Remove(homework);
        await Task.CompletedTask;
    }

    public async Task<Homework?> SelectByIdAsync(Guid courseId, Guid id)
    {
        return await _homeworks
            .AsNoTracking()
            .Where(h => h.CourseId == courseId)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Homework?> SelectAsync(Guid courseId, Expression<Func<Homework, bool>> predicate)
    {
        return await _homeworks
            .AsNoTracking()
            .Where(h => h.CourseId == courseId)
            .FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Homework>> SelectAllAsync(Guid courseId, Expression<Func<Homework, bool>>? predicate = null)
    {
        var query = _homeworks
            .AsNoTracking()
            .Where(h => h.CourseId == courseId);

        if (predicate is not null)
            query = query.Where(predicate);

        return await query.ToListAsync();
    }

    public IQueryable<Homework> SelectAllAsQueryable(Guid courseId, Expression<Func<Homework, bool>>? predicate = null)
    {
        var query = _homeworks
            .AsNoTracking()
            .Where(h => h.CourseId == courseId);

        if (predicate is not null)
            query = query.Where(predicate);

        return query;
    }
}
