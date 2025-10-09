using HomeworkModule.Domain.Repositories;

namespace HomeworkModule.Infrastructure.Repositories;

public class HomeworkUnitOfWork : IHomeworkUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
