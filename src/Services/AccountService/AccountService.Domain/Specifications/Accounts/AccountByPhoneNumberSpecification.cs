using AccountService.Domain.Aggregates;

namespace AccountService.Domain.Specifications.Accounts;

public class AccountByPhoneNumberSpecification : BaseSpecification<AccountEntity>
{
    public AccountByPhoneNumberSpecification(string PhoneNumber)
    {
        Criteria = account => account.PhoneNumber.Value == PhoneNumber;
    }
}
