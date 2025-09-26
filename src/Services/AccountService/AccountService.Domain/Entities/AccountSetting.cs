using AccountService.Domain.Aggregates;
using AccountService.Domain.Enums;
using AccountService.Domain.Interfaces;
using AccountService.Domain.States;
using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.Entities;

public class AccountSetting : Entity
{
    private IAccountStatusState _accountStatusState = new PendingAccountState();

    private AccountSetting(
        Guid id,
        Guid accountId,
        AccountEntity accountEntity,
        AccountStatus accountStatus)
    {
        Id = id;
        AccountId = accountId;
        Account = accountEntity;
        Status = accountStatus;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid AccountId { get; private set; }
    public AccountEntity? Account { get; private set; }
    public AccountStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsActived { get; private set; } = false;

    public static Result<AccountSetting> AttachToAccount(
        Guid id,
        Guid accountId,
        AccountEntity accountEntity,
        AccountStatus accountStatus)
    {
        return Result.Success(new AccountSetting(id, accountId, accountEntity, accountStatus));
    }

    public void Update(DateTime updatedTime)
        => UpdatedAt = updatedTime;

    public void SetState(IAccountStatusState state) => _accountStatusState = state;

    internal void ChangeStatus(AccountStatus status) => Status = status;

    public void Activate() => _accountStatusState.Activate(this);
    public void Deactivate() => _accountStatusState.Deactivate(this);
    public void Suspend() => _accountStatusState.Suspend(this);
    public void Close() => _accountStatusState.Close(this);
    public void Lock() => _accountStatusState.Lock(this);
}
