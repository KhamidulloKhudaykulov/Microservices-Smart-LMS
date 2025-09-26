using AccountService.Domain.Aggregates;
using SharedKernel.Domain.Repositories;

namespace AccountService.Domain.Repsoitories;

public interface IAccountRepository : IRepository<AccountEntity>
{
}
