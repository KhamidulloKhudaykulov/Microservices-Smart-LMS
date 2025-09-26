using AccountService.Domain.Entities;

namespace AccountService.Domain.Interfaces;

public interface IAccountStatusState
{
    void Activate(AccountSetting account);
    void Deactivate(AccountSetting account);
    void Suspend(AccountSetting account);
    void Close(AccountSetting account);
    void Lock(AccountSetting account);
}
