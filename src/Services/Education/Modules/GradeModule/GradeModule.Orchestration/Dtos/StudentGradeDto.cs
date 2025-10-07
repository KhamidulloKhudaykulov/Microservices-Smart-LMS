namespace GradeModule.Orchestration.Dtos;

public record StudentGradeDto(
    string StudentFullname,
    DateTime LessonDate,
    decimal Score,
    string? Feedback);
