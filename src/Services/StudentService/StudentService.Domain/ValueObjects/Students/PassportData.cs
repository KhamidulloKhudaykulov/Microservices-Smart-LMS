using System.Text.RegularExpressions;
using StudentService.Domain.Primitives;

namespace StudentService.Domain.ValueObjects.Students;

public class PassportData : ValueObject
{
    private PassportData(string value)
    {
        Value = value;
    }
    string Value { get; init; }

    public static Result<PassportData> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<PassportData>(new Error(
                "PassportData.IsEmpty", "PassportData can not be null or empty"));
        }
        
        // Regex: 2 ta katta harf + 7 ta raqam (AA1234567)
        var pattern = @"^[A-Z]{2}\d{7}$";
        if (!Regex.IsMatch(value, pattern))
        {
            return Result.Failure<PassportData>(
                new Error("PassportData.InvalidFormat", "Pasport raqami noto‘g‘ri formatda (masalan: AA1234567)."));
        }

        return new PassportData(value);
    }
    
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}