using AccountService.Application.UseCases.Accounts.Commands;
using AccountService.Domain.Repositories;
using AccountService.Domain.Specifications.Accounts;
using AccountService.Domain.ValueObjects.Accounts;
using SharedKernel.Application.Common.Rules;

namespace AccountService.Application.UseCases.Accounts.Rules;

public class UpdateAccountMustBeSuccess(
    IAccountRepository _accountRepository)
    : RuleBase<UpdateAccountCommand>
{
    protected override async Task<Result> HandleAsync(UpdateAccountCommand command, CancellationToken cancellationToken)
    {
        var accountSpecification = new AccountByIdSpecification(command.AccountId);

        var account = await _accountRepository
            .SelectAsync(accountSpecification);

        if (account is null)
            return Result.Failure(new Error(
                code: "Account.NotFound",
                message: "Account not found"));

        Result<AccountName>? accountNameResult = null;
        Result<PhoneNumber>? phoneNumberResult = null;
        Result<Email>? emailResult = null;

        if (!string.IsNullOrEmpty(command.Accountname))
        {
            accountNameResult = AccountName.Create(command.Accountname);
            if (!accountNameResult.IsSuccess)
                return await Task.FromResult(Result.Failure(accountNameResult.Error));
        }

        if (!string.IsNullOrEmpty(command.PhoneNumber))
        {
            phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
            if (!phoneNumberResult.IsSuccess)
                return await Task.FromResult(Result.Failure(phoneNumberResult.Error));
        }

        if (!string.IsNullOrEmpty(command.Email))
        {
            emailResult = Email.Create(command.Email);
            if (!emailResult.IsSuccess)
                return await Task.FromResult(Result.Failure(emailResult.Error));
        }

        // Entity update
        account.Update(
            accountNameResult?.Value,
            phoneNumberResult?.Value,
            emailResult?.Value
        );

        await _accountRepository.UpdateAsync(account);

        return await Task.FromResult(Result.Success());
    }
}
