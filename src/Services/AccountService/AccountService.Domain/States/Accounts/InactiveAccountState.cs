using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.Interfaces;
using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.States;

public class InactiveAccountState : IAccountStatusState
{
    public void Activate(AccountEntity account)
    {
        account.SetState(new ActiveAccountState());
        account.ChangeStatus(AccountStatus.Active);
    }

    public void Deactivate(AccountEntity account)
    {
        // Allaqachon inactive
        Result.Failure(new Error(
            code: "Account.AlreadyInactive",
            message: "This account is already inactive"));
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
