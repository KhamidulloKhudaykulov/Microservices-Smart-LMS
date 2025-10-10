using ScheduleModule.Domain.Repositories;
using ScheduleModule.Infrastructure.Persistence;

namespace ScheduleModule.Infrastructure.Repositories;

public class ScheduleUnitOfWork : IScheduleUnitOfWork
{
    private readonly ScheduleDbContext _dbContext;

    public ScheduleUnitOfWork(ScheduleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
