using AccountService.Domain.Enums;
using AccountService.Domain.Interfaces;
using AccountService.Domain.States;
using AccountService.Domain.ValueObjects.Accounts;
using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.Entities;

public class AccountEntity : Entity
{
    private IAccountStatusState _accountStatusState = new PendingAccountState();

    private AccountEntity(
        Guid id,
        AccountName accountName,
        PhoneNumber phoneNumber,
        Email email,
        DateTime createdAt,
        AccountStatus accountStatus,
        Address? address = null)
        : base(id)
    {
        AccountName = accountName;
        PhoneNumber = phoneNumber;
        Email = email;
        CreatedAt = createdAt;
        AccountStatus = accountStatus;
        Address = address;
        AddressId = address?.Id;
    }

    public AccountName AccountName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Email Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public AccountStatus AccountStatus { get; private set; }
    public Guid? AddressId { get; private set; }
    public Address? Address { get; private set; }

    public static Result<AccountEntity> Create(
        AccountName accountName,
        PhoneNumber phoneNumber,
        Email email,
        Address? address = null)
    {
        if (accountName == null)
            return Result.Failure<AccountEntity>(new Error("AccountName.Null", "AccountName is required"));

        if (phoneNumber == null)
            return Result.Failure<AccountEntity>(new Error("PhoneNumber.Null", "PhoneNumber is required"));

        if (email == null)
            return Result.Failure<AccountEntity>(new Error("Email.Null", "Email is required"));

        var account = new AccountEntity(
            Guid.NewGuid(),
            accountName,
            phoneNumber,
            email,
            DateTime.UtcNow,
            AccountStatus.Pending,
            address);

        return Result.Success(account);
    }

    public void SetState(IAccountStatusState state) => _accountStatusState = state;

    internal void ChangeStatus(AccountStatus status) => AccountStatus = status;

    public void Activate() => _accountStatusState.Activate(this);
    public void Deactivate() => _accountStatusState.Deactivate(this);
    public void Suspend() => _accountStatusState.Suspend(this);
    public void Close() => _accountStatusState.Close(this);
    public void Lock() => _accountStatusState.Lock(this);

    public void UpdateAddress(Address? address)
    {
        Address = address;
        AddressId = address?.Id;
    }
}
