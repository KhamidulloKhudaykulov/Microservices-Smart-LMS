using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.Interfaces;
using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.States;

public class PendingAccountState : IAccountStatusState
{
    public void Activate(AccountEntity account)
    {
        account.SetState(new ActiveAccountState());
        account.ChangeStatus(AccountStatus.Active);
    }

    public void Deactivate(AccountEntity account)
    {
        account.SetState(new InactiveAccountState());
        account.ChangeStatus(AccountStatus.Inactive);
    }

    public void Suspend(AccountEntity account)
    {
        // Pending accountni suspend qilish mantiqsiz
        Result.Failure(new Error(
            code: "Account.PendingCannotSuspend",
            message: "Pending account cannot be suspended"));
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
