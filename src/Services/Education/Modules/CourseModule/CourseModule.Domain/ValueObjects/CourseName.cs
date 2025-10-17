using SharedKernel.Domain.Primitives;

namespace CourseModule.Domain.ValueObjects;

public class CourseName : ValueObject
{
    public const int MAX_SIZE = 25;
    public const int MIN_SIZE = 2;
    private CourseName(string value)
        => Value = value;
    public string Value { get; private set; }
    public static Result<CourseName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<CourseName>(new Error(
                code: "Argument.NullOrEmpty",
                message: $"The specified value of course name can't be null or empty"));
        }

        if (value.Length < MIN_SIZE)
        {
            return Result.Failure<CourseName>(new Error(
                code: "Argument.TooShort",
                message: $"The specified value of course name can't be shorter than 2 characters"));
        }

        if (value.Length > MAX_SIZE)
        {
            return Result.Failure<CourseName>(new Error(
                code: "Argument.TooLong",
                message: $"The specified value of course name can't be longer than 25 characters"));
        }

        return Result.Success(new CourseName(value));
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
