using SharedKernel.Domain.Primitives;

namespace LessonModule.Domain.ValueObjects.Lessons;

public class Theme : ValueObject
{
    public string Value { get; }

    private Theme(string value)
    {
        Value = value;
    }

    public static Result<Theme> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Theme>(new Error(
                code: "Theme.EmptyOrNull",
                message: "Theme cannot be empty."));

        if (value.Length > 200)
            return Result.Failure<Theme>(new Error(
                code: "Argument.TooLong",
                message: "Theme is too long."));

        return Result.Success(new Theme(value.Trim()));
    }

    public override string ToString() => Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
