using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.Interfaces;

namespace AccountService.Domain.States;

public class ActiveAccountState : IAccountStatusState
{
    public void Activate(AccountEntity account)
    {
        // Allaqachon Active
        Result.Failure(new Error(
            code: "Account.AlreadyActive",
            message: "This account is already active"));
    }

    public void Deactivate(AccountEntity account)
    {
        account.SetState(new InactiveAccountState());
        account.ChangeStatus(AccountStatus.Inactive);
    }

    public void Suspend(AccountEntity account)
    {
        account.SetState(new SuspendedAccountState());
        account.ChangeStatus(AccountStatus.Suspended);
    }

    public void Close(AccountEntity account)
    {
        account.SetState(new ClosedAccountState());
        account.ChangeStatus(AccountStatus.Closed);
    }

    public void Lock(AccountEntity account)
    {
        account.SetState(new LockedAccountState());
        account.ChangeStatus(AccountStatus.Locked);
    }
}
