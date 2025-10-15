using SharedKernel.Domain.Repositories;

namespace TeacherService.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TeacherServiceDbContext _dbContext;

    public UnitOfWork(TeacherServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
