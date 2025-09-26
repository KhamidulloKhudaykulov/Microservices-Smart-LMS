using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
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
        var account = new AccountEntity(
            Guid.NewGuid(),
            accountName,
            phoneNumber,
            email,
            address);

        return Result.Success(account);
    }

    public Result<AccountEntity> Update(
        AccountName? accountName, 
        PhoneNumber? phoneNumber, 
        Email? email)
    {
        if (accountName != null)
            AccountName = accountName;

        if (phoneNumber != null)
            PhoneNumber = phoneNumber;

        if (email != null) 
            Email = email;

        return this;
    }

    public void UpdateAddress(Address? address)
    {
        Address = address;
        AddressId = address?.Id;
    }

    public Result<AccountSetting> AttachAccountSetting(AccountStatus status)
    {
        if (AccountSetting != null)
            return Result.Failure<AccountSetting>(new Error("AccountSetting.Exists", "Account already has settings"));

        var setting = AccountSetting.AttachToAccount(Guid.NewGuid(), Id, status);

        AccountSetting = setting.Value;
        AccountSettingId = setting.Value.Id;

        return setting;
    }
}
