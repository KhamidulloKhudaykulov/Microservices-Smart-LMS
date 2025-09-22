using System.Text.RegularExpressions;
using StudentService.Domain.Primitives;

namespace StudentService.Domain.ValueObjects.Students;

public class PhoneNumber : ValueObject
{

    private PhoneNumber(string value)
    {
        Value = value;
    }
    string Value { get; init; }

    public static Result<ValueObject> Create(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return Result.Failure<ValueObject>(
                new Error("PhoneNumber.Empty", "Telefon raqam bo‘sh bo‘lishi mumkin emas."));
        }

        // Regex: +998901234567 yoki 998901234567
        var uzbekPhonePattern = @"^(\+998|998)(\d{9})$";
        if (!Regex.IsMatch(value, uzbekPhonePattern))
        {
            return Result.Failure<ValueObject>(
                new Error("PhoneNumber.InvalidFormat", "Telefon raqam formati noto‘g‘ri. (+998901234567)"));
        }

        return new PhoneNumber(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}