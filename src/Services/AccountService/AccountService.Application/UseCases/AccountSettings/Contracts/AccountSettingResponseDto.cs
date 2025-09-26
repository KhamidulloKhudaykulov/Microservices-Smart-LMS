namespace AccountService.Application.UseCases.AccountSettings.Contracts;

public class AccountSettingResponseDto
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsActived { get; set; }
}
