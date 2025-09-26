using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.Interfaces;

namespace AccountService.Domain.States.AccountSettings;

public class InactiveAccountState : IAccountStatusState
{
    public void Activate(AccountSetting account)
    {
        account.SetState(new ActiveAccountState());
        account.ChangeStatus(AccountStatus.Active);
    }

    public void Deactivate(AccountSetting account)
    {
        // Allaqachon inactive
        Result.Failure(new Error(
            code: "Account.AlreadyInactive",
            message: "This account is already inactive"));
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
        account.SetState(new LockedAccountState());
        account.ChangeStatus(AccountStatus.Locked);
    }
}
