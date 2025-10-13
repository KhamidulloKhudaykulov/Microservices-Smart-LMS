namespace Integration.Logic.Dtos;

public record LessonResponseDto(
    Guid CourseId,
    string Theme,
    DateTime Date);
