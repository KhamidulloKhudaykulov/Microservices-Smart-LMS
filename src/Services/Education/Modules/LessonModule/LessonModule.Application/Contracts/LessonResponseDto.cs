namespace LessonModule.Application.Contracts;

public record LessonResponseDto(
    Guid CourseId,
    string Theme,
    DateTime Date);
