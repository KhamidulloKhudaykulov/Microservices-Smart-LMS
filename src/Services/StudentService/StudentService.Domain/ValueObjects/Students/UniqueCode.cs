using StudentService.Domain.Primitives;
using System.Text.RegularExpressions;

namespace StudentService.Domain.ValueObjects.Students;

public class UniqueCode : ValueObject
{
    private static readonly Regex CodePattern = new(@"^[A-Z]{2,3}[0-9]{4}$", RegexOptions.Compiled);
    public string Value { get; }

    private UniqueCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Code cannot be empty.", nameof(value));

        if (!CodePattern.IsMatch(value))
            throw new ArgumentException("Invalid code format. Expected like 'XY1234' or 'XYZ1234'.", nameof(value));

        Value = value;
    }

    public static Result<UniqueCode> Create()
    {
        var random = new Random();
        int lettersCount = random.Next(2, 4);
        string letters = string.Empty;

        for (int i = 0; i < lettersCount; i++)
            letters += (char)random.Next('A', 'Z' + 1);

        string numbers = random.Next(1000, 9999).ToString();
        var code = $"{letters}{numbers}";

        try
        {
            var uniqueCode = new UniqueCode(code);
            return Result.Success(uniqueCode);
        }
        catch (Exception ex)
        {
            return Result.Failure<UniqueCode>(new Error(
                code: ex.Message,
                message: ex.Message));
        }
    }

    public static Result<UniqueCode> From(string value)
    {
        try
        {
            var code = new UniqueCode(value);
            return Result.Success(code);
        }
        catch (Exception ex)
        {
            return Result.Failure<UniqueCode>(new Error(
                code: ex.Message,
                message: ex.Message));
        }
    }

    public override string ToString() => Value;

    public bool Equals(UniqueCode? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
