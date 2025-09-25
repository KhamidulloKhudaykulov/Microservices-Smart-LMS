using AccountService.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace AccountService.Domain.Repsoitories;

public interface IAccountRepository : IRepository<AccountEntity>
{
}
