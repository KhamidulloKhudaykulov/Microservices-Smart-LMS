using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.Interfaces;

namespace AccountService.Domain.States;

public class LockedAccountState : IAccountStatusState
{
    public void Activate(AccountSetting account)
    {
        account.SetState(new ActiveAccountState());
        account.ChangeStatus(AccountStatus.Active);
    }

    public void Deactivate(AccountSetting account)
    {
        account.SetState(new InactiveAccountState());
        account.ChangeStatus(AccountStatus.Inactive);
    }

    public void Suspend(AccountSetting account)
    {
        account.SetState(new SuspendedAccountState());
        account.ChangeStatus(AccountStatus.Suspended);
    }

    public void Close(AccountSetting account)
    {
        account.SetState(new ClosedAccountState());
        account.ChangeStatus(AccountStatus.Closed);
    }

    public void Lock(AccountSetting account)
    {
        // Allaqachon locked
        Result.Failure(new Error(
            code: "Account.AlreadyLocked",
            message: "This account is already locked"));
    }
}
