using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.Interfaces;

namespace AccountService.Domain.States.AccountSettings;

public class PendingAccountState : IAccountStatusState
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
        // Pending accountni suspend qilish mantiqsiz
        Result.Failure(new Error(
            code: "Account.PendingCannotSuspend",
            message: "Pending account cannot be suspended"));
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
