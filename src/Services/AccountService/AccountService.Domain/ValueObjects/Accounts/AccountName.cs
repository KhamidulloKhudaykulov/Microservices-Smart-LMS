using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.ValueObjects.Accounts;

public class AccountName : ValueObject
{
    private AccountName(string value)
        => Value = value;

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<AccountName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<AccountName>(new Error(
                code: "AccountName.IsNullOrEmpty",
                message: "Accountname can't be null or empty"));
        }

        if (value.Length > 60)
        {
            return Result.Failure<AccountName>(new Error(
                code: "Value.TooLong",
                message: "The specific value can't be longer than 60 characters"));
        }

        return Result.Success(new AccountName(value));
    }
}
