using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects.Accounts;
using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.Aggregates;

public class AccountEntity : AggregateRoot
{
    private AccountEntity(
        Guid id,
        AccountName accountName,
        PhoneNumber phoneNumber,
        Email email,
        Address? address = null)
        : base(id)
    {
        AccountName = accountName;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        AddressId = address?.Id;
    }

    public AccountName AccountName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Email Email { get; private set; }

    public Guid? AddressId { get; private set; }
    public Address? Address { get; private set; }

    public Guid? AccountSettingId { get; private set; }
    public AccountSetting? AccountSetting { get; private set; }

    public static Result<AccountEntity> Create(
        AccountName accountName,
        PhoneNumber phoneNumber,
        Email email,
        Address? address = null)
    {
        if (accountName == null)
            return Result.Failure<AccountEntity>(new Error(
                "AccountName.Null",
                "AccountName is required"));

        if (phoneNumber == null)
            return Result.Failure<AccountEntity>(new Error(
                "PhoneNumber.Null", 
                "PhoneNumber is required"));

        if (email == null)
            return Result.Failure<AccountEntity>(new Error(
                "Email.Null", 
                "Email is required"));

        var account = new AccountEntity(
            Guid.NewGuid(),
            accountName,
            phoneNumber,
            email,
            address);

        return Result.Success(account);
    }

    public void UpdateAddress(Address? address)
    {
        Address = address;
        AddressId = address?.Id;
    }

    public void AttachAccountSetting(AccountSetting accountSetting)
    {
        AccountSetting = accountSetting;
        AccountSettingId = accountSetting.Id;
    }
}
