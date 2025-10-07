using LessonModule.Domain.Entities;
using LessonModule.Domain.Repositories;
using LessonModule.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Specifications;

namespace LessonModule.Infrastructure.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly LessonDbContext _dbContext;
    private readonly DbSet<LessonEntity> _lessons;

    public LessonRepository(LessonDbContext dbContext)
    {
        _dbContext = dbContext;
        _lessons = _dbContext.Set<LessonEntity>();
    }

    public async Task<LessonEntity?> SelectByIdAsync(Guid id)
    {
        return await _lessons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<LessonEntity> InsertAsync(LessonEntity entity)
    {
        return (await _lessons.AddAsync(entity)).Entity;
    }

    public async Task<LessonEntity> UpdateAsync(LessonEntity entity)
    {
        _lessons.Update(entity);
        return await Task.FromResult(entity);
    }

    public async Task DeleteAsync(LessonEntity entity)
    {
        _lessons.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<int> CountAsync(ISpecification<LessonEntity> specification)
    {
        return await ApplySpecification(specification).CountAsync();
    }

    public async Task<IReadOnlyList<LessonEntity>> ListAsync(ISpecification<LessonEntity> specification)
    {
        return await ApplySpecification(specification)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<LessonEntity?> SelectAsync(ISpecification<LessonEntity> specification)
    {
        return await ApplySpecification(specification)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    private IQueryable<LessonEntity> ApplySpecification(ISpecification<LessonEntity> specification)
    {
        return SpecificationEvaluator.GetQuery(_lessons.AsQueryable(), specification);
    }
}
