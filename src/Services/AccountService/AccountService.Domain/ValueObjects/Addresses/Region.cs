using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.ValueObjects.Addresses;

public class Region : ValueObject
{
    private Region(string value) => Value = value;

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<Region> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Region>(new Error(
                code: "Region.Empty",
                message: "Region cannot be empty"));
        }

        if (value.Length > 100)
        {
            return Result.Failure<Region>(new Error(
                code: "Region.TooLong",
                message: "Region name cannot exceed 100 characters"));
        }

        return Result.Success(new Region(value));
    }

    public override string ToString() => Value;
}
