using AccountService.Domain.Repositories;
using AccountService.Domain.Specifications.Accounts;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace AccountService.Application.UseCases.AccountSettings.Commands;

public record SuspendAccountCommand(Guid AccountId) : ICommand<Unit>;

public class SuspendAccountCommandHandler
    : ICommandHandler<SuspendAccountCommand, Unit>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SuspendAccountCommandHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(SuspendAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.SelectAsync(new AccountByIdSpecification(request.AccountId));
        if (account == null)
            return Result.Failure<Unit>(new Error("Account.NotFound", "Account not found"));

        var setting = account.AccountSetting;
        if (setting == null)
            return Result.Failure<Unit>(new Error("AccountSetting.NotFound", "AccountSetting not found"));

        setting.Suspend();
        setting.Update(DateTime.UtcNow);

        await _accountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
