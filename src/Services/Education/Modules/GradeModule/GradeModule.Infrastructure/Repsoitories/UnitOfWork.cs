using GradeModule.Domain.Repositories;
using GradeModule.Infrastructure.Persistence;

namespace GradeModule.Infrastructure.Repsoitories;

public class UnitOfWork : IGradeUnitOfWork
{
    private readonly GradeDbContext _dbContext;

    public UnitOfWork(GradeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
