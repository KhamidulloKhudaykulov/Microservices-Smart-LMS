using AccountService.Application.UseCases.Accounts.Rules;
using AccountService.Domain.Aggregates;
using AccountService.Domain.Repositories;
using AccountService.Domain.ValueObjects.Accounts;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace AccountService.Application.UseCases.Accounts.Commands;

public record CreateAccountCommand(
    string AccountName,
    string PhoneNumber,
    string Email) : ICommand<Unit>;

public class CreateAccountCommandHandler(
    IAccountRepository _accountRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<CreateAccountCommand, Unit>
{
    public async Task<Result<Unit>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var rules = new CreateAccountMustSuccessRule(_accountRepository);

        var validationResult = await rules.CheckAsync(request);
        if (validationResult.IsFailure)
            return Result.Failure<Unit>(validationResult.Error);

        var accountNameResult = AccountName.Create(request.AccountName);
        if (!accountNameResult.IsSuccess)
            return Result.Failure<Unit>(accountNameResult.Error);

        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
        if (!phoneNumberResult.IsSuccess)
            return Result.Failure<Unit>(phoneNumberResult.Error);

        var emailResult = Email.Create(request.Email);
        if (!emailResult.IsSuccess)
            return Result.Failure<Unit>(emailResult.Error);

        var accountResult = AccountEntity.Create(
            accountNameResult.Value,
            phoneNumberResult.Value,
            emailResult.Value);

        if (!accountResult.IsSuccess)
            return Result.Failure<Unit>(accountResult.Error);

        await _accountRepository.InsertAsync(accountResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
