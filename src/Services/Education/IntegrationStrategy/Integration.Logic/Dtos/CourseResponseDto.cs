namespace CourseModule.Application.UseCases.Courses.Contracts;

public record CourseResponseDto(
    Guid Id,
    string Name,
    DateTime StartsAt);
