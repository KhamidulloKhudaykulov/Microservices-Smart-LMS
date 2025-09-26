using AccountService.Domain.Enums;
using AccountService.Domain.ValueObjects.Addresses;
using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.Entities;

public class Address : Entity
{
    private Address(
        Guid id,
        Street street,
        City city,
        Region region,
        Guid accountId)
        : base(id)
    {
        Street = street;
        City = city;
        Region = region;
        AccountId = accountId;
    }

    public Street Street { get; private set; }
    public City City { get; private set; }
    public Region Region { get; private set; }

    // One-to-One relationship with AccountEntity
    public Guid AccountId { get; private set; }

    public void Update(
        Street street,
        City city,
        Region region)
    {
        Street = street;
        City = city;
        Region = region;
    }

    public static Result<Address> Create(
        Street street,
        City city,
        Region region,
        Guid accountId)
    {
        var address = new Address(
            Guid.NewGuid(),
            street,
            city,
            region,
            accountId);

        return Result.Success(address);
    }
}
