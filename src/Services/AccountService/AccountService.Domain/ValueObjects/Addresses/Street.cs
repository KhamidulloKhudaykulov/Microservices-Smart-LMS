using SharedKernel.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.ValueObjects.Addresses;

public class Street : ValueObject
{
    private Street(string value) => Value = value;

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<Street> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Street>(new Error(
                code: "Street.Empty",
                message: "Street cannot be empty"));
        }

        if (value.Length > 100)
        {
            return Result.Failure<Street>(new Error(
                code: "Street.TooLong",
                message: "Street name cannot exceed 100 characters"));
        }

        return Result.Success(new Street(value));
    }

    public override string ToString() => Value;
}

