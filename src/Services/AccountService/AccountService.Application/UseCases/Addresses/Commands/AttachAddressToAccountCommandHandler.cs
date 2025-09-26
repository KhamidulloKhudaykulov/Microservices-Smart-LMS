using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.Repositories;
using AccountService.Domain.Specifications.Accounts;
using AccountService.Domain.ValueObjects.Addresses;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace AccountService.Application.UseCases.Addresses.Commands;

public record AttachAddressToAccountCommand(
    Guid AccountId,
    string Street,
    City City,
    string Region) 
    : ICommand<Unit>;

public class AttachAddressToAccountCommandHandler(
    IAccountRepository _accountRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<AttachAddressToAccountCommand, Unit>
{
    public async Task<Result<Unit>> Handle(AttachAddressToAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.SelectAsync(new AccountByIdSpecification(request.AccountId));
        if (account == null)
            return Result.Failure<Unit>(new Error(
                code: "Account.NotFound",
                message: "Account not found"));

        var street = Street.Create(request.Street);
        if (street.IsFailure)
            return Result.Failure<Unit>(street.Error);

        var region = Region.Create(request.Region);
        if (region.IsFailure)
            return Result.Failure<Unit>(region.Error);

        var address = Address.Create(street.Value, request.City, region.Value);
        
        account.UpdateAddress(address.Value);

        await _accountRepository.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
