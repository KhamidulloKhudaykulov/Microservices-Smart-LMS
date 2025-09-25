using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.ValueObjects.Accounts;

public class Email : ValueObject
{
    private Email(string value)
        => Value = value;

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Email>(new Error(
                code: "Email.IsNullOrEmpty",
                message: "Email can't be null or empty"));
        }

        if (value.Length > 254)
        {
            return Result.Failure<Email>(new Error(
                code: "Email.TooLong",
                message: "Email can't be longer than 254 characters"));
        }

        var regex = new System.Text.RegularExpressions.Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            System.Text.RegularExpressions.RegexOptions.Compiled |
            System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        if (!regex.IsMatch(value))
        {
            return Result.Failure<Email>(new Error(
                code: "Email.InvalidFormat",
                message: "Email format is invalid"));
        }

        return Result.Success(new Email(value));
    }
}
