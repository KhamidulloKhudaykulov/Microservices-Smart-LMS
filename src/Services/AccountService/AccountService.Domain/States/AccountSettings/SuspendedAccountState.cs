using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.Interfaces;

namespace AccountService.Domain.States.AccountSettings;

public class SuspendedAccountState : IAccountStatusState
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
        // Allaqachon suspended
        Result.Failure(new Error(
            code: "Account.AlreadySuspended",
            message: "This account is already suspended"));
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
