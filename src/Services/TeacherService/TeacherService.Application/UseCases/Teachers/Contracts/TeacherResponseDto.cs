namespace TeacherService.Application.UseCases.Teachers.Contracts;

public record TeacherResponseDto(
    Guid Id,
    string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    string Status);
