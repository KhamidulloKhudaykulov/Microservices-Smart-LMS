using AccountService.Domain.Enums;
using AccountService.Domain.Repositories;
using MediatR;
using SharedKernel.Domain.Repositories;

namespace AccountService.Application.UseCases.AccountSettings.Commands;

public record AttachSettingsToAccountCommand(
    Guid AccountId,
    AccountStatus AccountStatus) : IRequest<Result>;


public class AttachSettingsToAccountCommandHandler(
    IAccountRepository _accountRepository,
    IUnitOfWork _unitOfWork) 
    : IRequestHandler<AttachSettingsToAccountCommand, Result>
{
    public async Task<Result> Handle(AttachSettingsToAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.SelectByIdAsync(request.AccountId);
        if (account is null)
            return Result.Failure(new Error(
                code: "Account.NotFound",
                message: "Account not found"));

        var settings = account.AttachAccountSetting(request.AccountStatus);

        await _accountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
