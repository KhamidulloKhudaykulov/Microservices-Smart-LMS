using AccountService.Application.UseCases.Accounts.Commands;
using AccountService.Domain.Aggregates;
using AccountService.Domain.Repositories;
using AccountService.Domain.Specifications.Accounts;
using AccountService.Domain.ValueObjects.Accounts;
using SharedKernel.Application.Common.Rules;

namespace AccountService.Application.UseCases.Accounts.Rules;

public class CreateAccountMustSuccessRule(
    IAccountRepository _accountRepository)
    : RuleBase<CreateAccountCommand>
{
    protected override async Task<Result> HandleAsync(
        CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        var byPhoneNumberSpec = new AccountByPhoneNumberSpecification(command.PhoneNumber);
        var byEmailSpec = new AccountByEmailSpecification(command.Email);

        var existingAccountWithPhoneNumber = await _accountRepository.SelectAsync(byPhoneNumberSpec);
        var existingAccountWithEmail = await _accountRepository.SelectAsync(byEmailSpec);

        if (existingAccountWithPhoneNumber is not null
            || existingAccountWithEmail is not null)
        {
            return Result.Failure(new Error(
                code: "Account.AlreadyExists",
                message: "An account with the provided phone number or email already exists."));
        }
        
        var account = AccountEntity.Create(
            AccountName.Create(command.Name).Value, 
            PhoneNumber.Create(command.PhoneNumber).Value, 
            Email.Create(command.Email).Value);

        if (account.IsFailure)
            return Result.Failure(account.Error);

        await _accountRepository.InsertAsync(account.Value);

        return Result.Success(account.Value);
    }
}
