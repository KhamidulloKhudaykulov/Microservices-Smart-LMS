using AccountService.Application.UseCases.Accounts.Rules;
using AccountService.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace AccountService.Application.UseCases.Accounts.Commands;

public record UpdateAccountCommand(
    Guid AccountId,
    string? Accountname,
    string? PhoneNumber,
    string? Email) : ICommand<Unit>;

public class UpdateAccountCommandHandler(
    IAccountRepository _accountRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<UpdateAccountCommand, Unit>
{
    public async Task<Result<Unit>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var rules = new UpdateAccountMustBeSuccess(_accountRepository);

        var validationResult = await rules.CheckAsync(request, cancellationToken);
        if (validationResult.IsFailure)
            return Result.Failure<Unit>(validationResult.Error);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
