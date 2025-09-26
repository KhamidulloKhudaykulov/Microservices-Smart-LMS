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
        Region region)
        : base(id)
    {
        Street = street;
        City = city;
        Region = region;
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
        Region region)
    {
        var address = new Address(
            Guid.NewGuid(),
            street,
            city,
            region);

        return Result.Success(address);
    }
}
