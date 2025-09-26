using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.Interfaces;

namespace AccountService.Domain.States;

public class ActiveAccountState : IAccountStatusState
{
    public void Activate(AccountSetting account)
    {
        // Allaqachon Active
        Result.Failure(new Error(
            code: "Account.AlreadyActive",
            message: "This account is already active"));
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
        account.SetState(new LockedAccountState());
        account.ChangeStatus(AccountStatus.Locked);
    }
}
