using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.ValueObjects.Accounts;

public class PhoneNumber : ValueObject
{
    private PhoneNumber(string value)
        => Value = value;

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<PhoneNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<PhoneNumber>(new Error(
                code: "PhoneNumber.IsNullOrEmpty",
                message: "Phone number can't be null or empty"));
        }

        // Tozalash: probel, tire va boshqa belgilarni olib tashlaymiz
        var normalized = value.Replace(" ", "")
                              .Replace("-", "")
                              .Replace("(", "")
                              .Replace(")", "");

        // Regex orqali tekshirish: faqat +998XXXXXXXXX formatida bo‘lishi kerak
        var regex = new System.Text.RegularExpressions.Regex(@"^\+998\d{9}$");
        if (!regex.IsMatch(normalized))
        {
            return Result.Failure<PhoneNumber>(new Error(
                code: "PhoneNumber.InvalidFormat",
                message: "Phone number must be in format +998XXXXXXXXX"));
        }

        return Result.Success(new PhoneNumber(normalized));
    }
}
