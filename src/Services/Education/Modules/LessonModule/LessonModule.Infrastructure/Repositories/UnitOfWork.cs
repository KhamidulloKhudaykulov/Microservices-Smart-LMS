using LessonModule.Domain.Repositories;
using LessonModule.Infrastructure.Persistence;
using SharedKernel.Domain.Repositories;

namespace LessonModule.Infrastructure.Repositories;

public class UnitOfWork : ILessonUnitOfWork
{
    private readonly LessonDbContext _dbContext;

    public UnitOfWork(LessonDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
