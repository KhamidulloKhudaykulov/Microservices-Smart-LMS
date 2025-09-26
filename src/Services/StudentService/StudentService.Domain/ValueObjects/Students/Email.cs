using StudentService.Domain.Primitives;
using System.Text.RegularExpressions;

namespace StudentService.Domain.ValueObjects.Students;

public class Email : ValueObject
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Email>(
                new Error("Email.Empty", "Email manzili bo‘sh bo‘lishi mumkin emas."));
        }

        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(value, emailPattern))
        {
            return Result.Failure<Email>(
                new Error("Email.InvalidFormat", "Email manzili formati noto‘g‘ri."));
        }

        return Result.Success(new Email(value));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}