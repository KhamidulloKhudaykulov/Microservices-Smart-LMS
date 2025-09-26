using AccountService.Domain.Aggregates;

namespace AccountService.Domain.Specifications.Accounts;

public class AccountByIdSpecification : BaseSpecification<AccountEntity>
{
    public AccountByIdSpecification(Guid id)
    {
        Criteria = account => account.Id == id;

        AddInclude(a => a.AccountSetting);

        AddInclude(a => a.Address);
    }
}
