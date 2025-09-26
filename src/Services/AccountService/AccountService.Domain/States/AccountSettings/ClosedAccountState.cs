using AccountService.Domain.Entities;
using AccountService.Domain.Interfaces;

namespace AccountService.Domain.States.AccountSettings;

public class ClosedAccountState : IAccountStatusState
{
    public void Activate(AccountSetting account)
    {
        // Yopilgan account faollashmaydi
        Result.Failure(new Error(
            code: "Account.Closed",
            message: "Closed account cannot be activated"));
    }

    public void Deactivate(AccountSetting account)
    {
        // Yopilgan account deaktivalanishi mumkin emas
        Result.Failure(new Error(
            code: "Account.Closed",
            message: "Closed account cannot be deactivated"));
    }

    public void Suspend(AccountSetting account)
    {
        Result.Failure(new Error(
            code: "Account.Closed",
            message: "Closed account cannot be suspended"));
    }

    public void Close(AccountSetting account)
    {
        // Allaqachon closed
        Result.Failure(new Error(
            code: "Account.AlreadyClosed",
            message: "This account is already closed"));
    }

    public void Lock(AccountSetting account)
    {
        Result.Failure(new Error(
            code: "Account.Closed",
            message: "Closed account cannot be locked"));
    }
}
