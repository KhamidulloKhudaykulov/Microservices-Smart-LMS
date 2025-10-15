using SharedKernel.Domain.Primitives;
using System.Text.RegularExpressions;

namespace TeacherService.Domain.ValueObjects.Teachers;

public class PhoneNumber : ValueObject
{

    private PhoneNumber(string value)
    {
        Value = value;
    }
    public string Value { get; init; }

    public static Result<PhoneNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<PhoneNumber>(
                new Error("PhoneNumber.Empty", "Telefon raqam bo‘sh bo‘lishi mumkin emas."));
        }

        // Regex: +998901234567 yoki 998901234567
        var uzbekPhonePattern = @"^(\+998|998)(\d{9})$";
        if (!Regex.IsMatch(value, uzbekPhonePattern))
        {
            return Result.Failure<PhoneNumber>(
                new Error("PhoneNumber.InvalidFormat", "Telefon raqam formati noto‘g‘ri. (+998901234567)"));
        }

        return Result.Success(new PhoneNumber(value));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}