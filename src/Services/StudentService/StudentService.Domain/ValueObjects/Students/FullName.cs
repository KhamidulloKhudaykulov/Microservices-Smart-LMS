using StudentService.Domain.Primitives;

namespace StudentService.Domain.ValueObjects.Students;

public class FullName : ValueObject
{
    private FullName(string value)
        => Value = value;
    
    public string Value { get; init; }

    public static Result<FullName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<FullName>(
                new Error(
                    "Fullname.NullValue", 
                    "The specific value can't be null or empty"));
        }
        return Result.Success(new FullName(value));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}