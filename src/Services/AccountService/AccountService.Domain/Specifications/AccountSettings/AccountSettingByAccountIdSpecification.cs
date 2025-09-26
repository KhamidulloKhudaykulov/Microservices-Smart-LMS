using AccountService.Domain.Entities;

namespace AccountService.Domain.Specifications.AccountSettings;

public class AccountSettingByAccountIdSpecification : BaseSpecification<AccountSetting>
{
    public AccountSettingByAccountIdSpecification(Guid accountId)
    {
        Criteria = setting => setting.AccountId == accountId;
    }
}
