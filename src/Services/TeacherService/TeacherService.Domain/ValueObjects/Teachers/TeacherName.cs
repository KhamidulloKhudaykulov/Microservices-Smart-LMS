using SharedKernel.Domain.Primitives;

namespace TeacherService.Domain.ValueObjects.Teachers;

public class TeacherName : ValueObject
{
    public const int MAX_LENGHT = 20;
    public const int MIN_LENGHT = 1;
    private TeacherName(string value)
        => Value = value;

    public string Value { get; private set; }

    public static Result<TeacherName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Failure<TeacherName>(new Error(
                code: "Value.NullOrEmpty",
                message: $"The specified value of name can't be null or empty"));
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<TeacherName>(new Error(
                code: "Value.NullOrWhiteSpace",
                message: $"The specified value of name can't be null or white space"));
        if (value.Length > MAX_LENGHT)
            return Result.Failure<TeacherName>(new Error(
                code: "Value.TooLong",
                message: $"The specified value of name can't be longer than 20 characters"));
        if (value.Length == MIN_LENGHT)
            return Result.Failure<TeacherName>(new Error(
                code: "Value.TooShort",
                message: $"The specified value of name can't be shorter than 2 characters"));

        return Result.Success(new TeacherName(value));

    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
