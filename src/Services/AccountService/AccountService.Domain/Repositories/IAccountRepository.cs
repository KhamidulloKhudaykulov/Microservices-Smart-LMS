using AccountService.Domain.Aggregates;
using SharedKernel.Domain.Repositories;

namespace AccountService.Domain.Repositories;

public interface IAccountRepository : IRepository<AccountEntity>
{
}
