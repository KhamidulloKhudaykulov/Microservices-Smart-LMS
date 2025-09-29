using AccountService.Application.UseCases.Accounts.Contracts;
using AccountService.Domain.Repositories;
using AccountService.Domain.Specifications.Accounts;
using SharedKernel.Application.Abstractions.Messaging;

namespace AccountService.Application.UseCases.Accounts.Queries;

public record GetAccountDataQuery(
    Guid AccountId) : IQuery<AccountDataResponseDto>;


public class GetAccountDataQueryHandler(
    IAccountRepository _accountRepository) 
    : IQueryHandler<GetAccountDataQuery, AccountDataResponseDto>
{
    public async Task<Result<AccountDataResponseDto>> Handle(GetAccountDataQuery request, CancellationToken cancellationToken)
    {
        var accountSpecification = new AccountByIdSpecification(request.AccountId);

        var existAccount = await _accountRepository.SelectAsync(accountSpecification);

        if (existAccount is null)
            return Result.Failure<AccountDataResponseDto>(new Error(
                code: "Account.NotFound",
                message: "This Account was not exists"));

        var result = new AccountDataResponseDto(
            existAccount.Id,
            existAccount.AccountName.Value,
            existAccount.PhoneNumber.Value,
            existAccount.Email.Value,
            existAccount.Address?.City.ToString());

        return Result.Success(result);
    }
}
