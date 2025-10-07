using System.Linq.Expressions;
using GradeModule.Domain.Enitites;
using GradeModule.Domain.Repositories;
using GradeModule.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GradeModule.Infrastructure.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly GradeDbContext _dbContext;
    private readonly DbSet<GradeEntity> _grades;

    public GradeRepository(GradeDbContext dbContext)
    {
        _dbContext = dbContext;
        _grades = _dbContext.Set<GradeEntity>();
    }

    public async Task<GradeEntity> InsertAsync(GradeEntity entity)
    {
        await _grades.AddAsync(entity);
        return entity;
    }

    public Task<GradeEntity> UpdateAsync(GradeEntity entity)
    {
        _grades.Update(entity);
        return Task.FromResult(entity);
    }

    public async Task DeleteAsync(GradeEntity entity)
    {
        _grades.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<GradeEntity?> SelectByIdAsync(Guid id)
    {
        return await _grades.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<GradeEntity?> SelectAsync(Expression<Func<GradeEntity, bool>> predicate)
    {
        return await _grades.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<GradeEntity>> SelectAllAsync(Expression<Func<GradeEntity, bool>>? predicate = null)
    {
        var query = _grades.AsNoTracking().AsQueryable();

        if (predicate is not null)
            query = query.Where(predicate);

        return await query.ToListAsync();
    }

    public IQueryable<GradeEntity> SelectAsQueryable(Expression<Func<GradeEntity, bool>>? predicate = null)
    {
        var query = _grades.AsNoTracking().AsQueryable();

        if (predicate is not null)
            query = query.Where(predicate);

        return query;
    }
}
