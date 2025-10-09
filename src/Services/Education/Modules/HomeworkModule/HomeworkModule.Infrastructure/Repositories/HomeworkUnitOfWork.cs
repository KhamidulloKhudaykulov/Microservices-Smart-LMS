using HomeworkModule.Domain.Repositories;
using HomeworkModule.Infrastructure.Persistence;

namespace HomeworkModule.Infrastructure.Repositories;

public class HomeworkUnitOfWork : IHomeworkUnitOfWork
{
    private readonly HomeworkDbContext _context;

    public HomeworkUnitOfWork(HomeworkDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
