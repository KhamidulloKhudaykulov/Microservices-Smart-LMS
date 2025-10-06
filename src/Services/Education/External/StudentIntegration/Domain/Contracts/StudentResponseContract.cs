namespace Domain.Contracts;

public class StudentResponseContract
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PassportData { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
