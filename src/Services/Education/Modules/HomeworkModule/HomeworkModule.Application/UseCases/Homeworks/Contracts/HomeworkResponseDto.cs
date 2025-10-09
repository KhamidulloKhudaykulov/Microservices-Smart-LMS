namespace HomeworkModule.Application.UseCases.Homeworks.Contracts;

public record HomeworkResponseDto(
    Guid Id,
    Guid LessonId,
    Guid CreatedBy,
    DateTime CreatedAt,
    DateTime EndTime,
    string Title,
    string? Description,
    decimal MaxScore,
    string Status);
