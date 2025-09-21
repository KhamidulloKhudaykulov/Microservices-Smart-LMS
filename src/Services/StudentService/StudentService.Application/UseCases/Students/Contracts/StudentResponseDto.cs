namespace StudentService.Application.UseCases.Students.Contracts;

public class StudentResponseDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }  = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PassportData { get; set; } = string.Empty;
}