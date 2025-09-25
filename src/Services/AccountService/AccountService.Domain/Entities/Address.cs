using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.ValueObjects.Addresses;
using SharedKernel.Domain.Primitives;
using System.IO;

namespace AccountService.Domain.Entities;

public class Address : Entity
{
    private Address(
        Guid id,
        Street street,
        City city,
        Region region,
        Guid? accountId = null)
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

    public Guid? AccountId { get; private set; }
    public AccountEntity? Account { get; private set; }

    public void Update(
        Street street,
        City city,
        Region region,
        string postalCode)
    {
        Street = street;
        City = city;
        Region = region;
    }

    public static Result<Address> Create(
        Street street,
        City city,
        Region region,
        Guid? accountId = null)
    {
        if (street is null)
            return Result.Failure<Address>(new Error(
                "Address.StreetNull",
                "Street must be provided"));

        if (region is null)
            return Result.Failure<Address>(new Error(
                "Address.RegionNull",
                "Region must be provided"));

        var address = new Address(
            Guid.NewGuid(),
            street,
            city,
            region,
            accountId);

        return Result.Success(address);
    }

    public void AttachToAccount(AccountEntity account)
    {
        Account = account;
        AccountId = account.Id;
    }
}
