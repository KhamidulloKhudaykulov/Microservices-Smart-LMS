using AccountService.Domain.Entities;
using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.Interfaces;

public interface IAccountStatusState
{
    void Activate(AccountEntity account);
    void Deactivate(AccountEntity account);
    void Suspend(AccountEntity account);
    void Close(AccountEntity account);
    void Lock(AccountEntity account);
}
