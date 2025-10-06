namespace CourseModel.Orchestration.Dtos;

public record StudentDto(
    Guid Id,
    string Fullname,
    string Email,
    string PassportData,
    string Phonenumber);
