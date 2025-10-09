using GradeModule.Domain.Enitites;
using GradeModule.Domain.Repositories;
using GradeModule.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GradeModule.Infrastructure.Repsoitories;

public class GradeHomeworkRepository : IGradeHomeworkRepository
{
    private readonly GradeDbContext _dbContext;
    private readonly DbSet<GradeHomeworkEntity> _grades;

    public GradeHomeworkRepository(GradeDbContext dbContext)
    {
        _dbContext = dbContext;
        _grades = _dbContext.Set<GradeHomeworkEntity>();
    }

    public async Task<GradeHomeworkEntity> InsertAsync(GradeHomeworkEntity entity)
    {
        await _grades.AddAsync(entity);
        return entity;
    }

    public Task<GradeHomeworkEntity> UpdateAsync(GradeHomeworkEntity entity)
    {
        _grades.Update(entity);
        return Task.FromResult(entity);
    }

    public async Task DeleteAsync(GradeHomeworkEntity entity)
    {
        _grades.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<GradeHomeworkEntity?> SelectByIdAsync(Guid id)
    {
        return await _grades.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<GradeHomeworkEntity?> SelectAsync(Expression<Func<GradeHomeworkEntity, bool>> predicate)
    {
        return await _grades.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<GradeHomeworkEntity>> SelectAllAsync(Expression<Func<GradeHomeworkEntity, bool>>? predicate = null)
    {
        var query = _grades.AsNoTracking().AsQueryable();

        if (predicate is not null)
            query = query.Where(predicate);

        return await query.ToListAsync();
    }

    public IQueryable<GradeHomeworkEntity> SelectAsQueryable(Expression<Func<GradeHomeworkEntity, bool>>? predicate = null)
    {
        var query = _grades.AsNoTracking().AsQueryable();

        if (predicate is not null)
            query = query.Where(predicate);

        return query;
    }
}
