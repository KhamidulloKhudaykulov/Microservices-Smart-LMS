using AccountService.Domain.Entities;
using AccountService.Domain.Interfaces;

namespace AccountService.Domain.States;

public class ClosedAccountState : IAccountStatusState
{
    public void Activate(AccountEntity account)
    {
        // Yopilgan account faollashmaydi
        Result.Failure(new Error(
            code: "Account.Closed",
            message: "Closed account cannot be activated"));
    }

    public void Deactivate(AccountEntity account)
    {
        // Yopilgan account deaktivalanishi mumkin emas
        Result.Failure(new Error(
            code: "Account.Closed",
            message: "Closed account cannot be deactivated"));
    }

    public void Suspend(AccountEntity account)
    {
        Result.Failure(new Error(
            code: "Account.Closed",
            message: "Closed account cannot be suspended"));
    }

    public void Close(AccountEntity account)
    {
        // Allaqachon closed
        Result.Failure(new Error(
            code: "Account.AlreadyClosed",
            message: "This account is already closed"));
    }

    public void Lock(AccountEntity account)
    {
        Result.Failure(new Error(
            code: "Account.Closed",
            message: "Closed account cannot be locked"));
    }
}
