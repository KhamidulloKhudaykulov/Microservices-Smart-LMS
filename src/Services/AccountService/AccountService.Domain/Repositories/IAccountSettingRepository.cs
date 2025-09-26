using AccountService.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace AccountService.Domain.Repositories;

public interface IAccountSettingRepository : IRepository<AccountSetting>
{
}
