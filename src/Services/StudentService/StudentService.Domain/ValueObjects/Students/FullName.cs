using StudentService.Domain.Primitives;

namespace StudentService.Domain.ValueObjects.Students;

public class FullName : ValueObject
{
    private FullName(string value)
        => Value = value;
    
    string Value { get; init; }

    public static Result<ValueObject> Create(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return Result.Failure<ValueObject>(new Error("Fullname.NullValue", "The specific value can't be null or empty"));
        }
        return new FullName(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}