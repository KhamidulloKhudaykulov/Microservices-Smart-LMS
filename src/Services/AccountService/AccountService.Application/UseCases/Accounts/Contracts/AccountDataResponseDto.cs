namespace AccountService.Application.UseCases.Accounts.Contracts;

public record AccountDataResponseDto(
    Guid Id,
    string AccountName,
    string PhoneNumber,
    string Email,
    string? Address);
