using CourseModule.Infrastructure.Persistence;
using SharedKernel.Domain.Repositories;

namespace CourseModule.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CourseDbContext _dbContext;

    public UnitOfWork(CourseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
