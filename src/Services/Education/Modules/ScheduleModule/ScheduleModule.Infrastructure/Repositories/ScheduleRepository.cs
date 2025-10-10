using Microsoft.EntityFrameworkCore;
using ScheduleModule.Domain.Repositories;
using ScheduleModule.Infrastructure.Persistence;
using SharedKernel.Domain.Primitives;
using System.Linq.Expressions;

namespace ScheduleModule.Infrastructure.Repositories;

public class ScheduleRepository<T> : IScheduleRepository<T>
    where T : Entity
{
    private readonly ScheduleDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public ScheduleRepository(ScheduleDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T> InsertAsync(T entity)
    {
        var entry = await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<T?> SelectByIdAsync(Guid id)
    {
        // T entity ning Id propertysi bo'lishi shart
        return await _dbSet.FindAsync(id);
    }

    public async Task<T?> SelectAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public IQueryable<T> SelectAllAsQueryable(Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = _dbSet.AsQueryable();
        if (predicate != null)
            query = query.Where(predicate);
        return query;
    }

    public async Task<IEnumerable<T>> SelectAllAsync(Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = _dbSet.AsQueryable();
        if (predicate != null)
            query = query.Where(predicate);
        return await query.ToListAsync();
    }
}
