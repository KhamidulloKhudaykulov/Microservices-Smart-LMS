using AccountService.Domain.Repositories;
using AccountService.Domain.Specifications.Accounts;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace AccountService.Application.UseCases.AccountSettings.Commands;

public record DeactivateAccountCommand(
    Guid AccountId) : ICommand<Unit>;

public class DeactivateAccountCommandHandler
    : ICommandHandler<DeactivateAccountCommand, Unit>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateAccountCommandHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(DeactivateAccountCommand request, CancellationToken cancellationToken)
    {
        var accountSpecification = new AccountByIdSpecification(request.AccountId);
        var accountResult = await _accountRepository.SelectAsync(accountSpecification);
        if (accountResult == null)
            return Result.Failure<Unit>(new Error(
                code: "Account.NotFound",
                message: "Account not found"));

        var setting = accountResult.AccountSetting;
        if (setting == null)
            return Result.Failure<Unit>(new Error(
                 code: "AccountSetting.NotFound",
                 message: "AccountSetting not found"));

        setting.Deactivate();

        setting.Update(DateTime.UtcNow);

        await _accountRepository.UpdateAsync(accountResult);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}