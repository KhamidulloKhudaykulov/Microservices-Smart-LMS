namespace LessonModule.Application.UseCases.Lessons.Contracts;

public record LessonResponseDto(
    Guid CourseId,
    Guid LessonId,
    TimeSpan StartsAt);
