using AccountService.Application.UseCases.AccountSettings.Contracts;
using AccountService.Domain.Repositories;
using AccountService.Domain.Specifications.AccountSettings;
using SharedKernel.Application.Abstractions.Messaging;

namespace AccountService.Application.UseCases.AccountSettings.Queries;

public record GetAccountSettingsQuery(
    Guid AccountId) : IQuery<AccountSettingResponseDto>;

public class GetAccountSettingsQueryHandler(
    IAccountSettingRepository _accountSettingRepository)
    : IQueryHandler<GetAccountSettingsQuery, AccountSettingResponseDto>
{
    public async Task<Result<AccountSettingResponseDto>> Handle(GetAccountSettingsQuery request, CancellationToken cancellationToken)
    {
        var specification = new AccountSettingByAccountIdSpecification(request.AccountId);

        var accountSetting = await _accountSettingRepository
            .ListAsync(specification);

        var response = accountSetting
            .Select(s => new AccountSettingResponseDto
            {
                Id = s.Id,
                AccountId = s.AccountId,
                Status = s.Status.ToString(),
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            })
            .FirstOrDefault();

        if (response is null)
            return Result.Failure<AccountSettingResponseDto>(
                new Error("AccountSetting.NotFound", "Account setting not found"));

        return Result.Success(response);
    }
}