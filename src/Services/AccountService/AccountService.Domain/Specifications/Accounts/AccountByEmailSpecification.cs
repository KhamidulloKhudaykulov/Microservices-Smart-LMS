using AccountService.Domain.Aggregates;

namespace AccountService.Domain.Specifications.Accounts;

public class AccountByEmailSpecification : BaseSpecification<AccountEntity>
{
    public AccountByEmailSpecification(string email)
    {
        Criteria = account => account.Email.Value == email;
    }
}
