using SharedKernel.Domain.Primitives;

namespace GradeModule.Domain.Enitites;

public class GradeEntity : Entity
{
    private GradeEntity(
        Guid courseId,
        Guid studentId,
        Guid? lessonId,
        Guid? examId,
        decimal score,
        Guid assignedBy,
        string? feedback)
    {
        CourseId = courseId;
        StudentId = studentId;
        LessonId = lessonId;
        ExamId = examId;
        Score = score;
        AssignedBy = assignedBy;
        Feedback = feedback;
        AssignedAt = DateTime.UtcNow;
    }
    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid? LessonId { get; private set; }
    public Guid? ExamId { get; private set; }
    public decimal Score { get; private set; }
    public DateTime AssignedAt { get; private set; }
    public Guid AssignedBy { get; private set; }
    public string? Feedback { get; private set; }

    public static Result<GradeEntity> Create(
        Guid courseId,
        Guid studentId,
        Guid? lessonId,
        Guid? examId,
        decimal score,
        Guid assignedBy,
        string? feedback = null)
    {
        if (studentId == Guid.Empty)
            return Result.Failure<GradeEntity>(new Error(
                code: "Argument.Empty",
                message: "StudentId cannot be empty"));

        if (assignedBy == Guid.Empty)
            return Result.Failure<GradeEntity>(new Error(
                code: "Argument.Empty",
                message: "AssignedBy cannot be empty"));

        if (score < 0 || score > 100)
            return Result.Failure<GradeEntity>(new Error(
                code: "Invalid.Argument",
                message: "Score must be between 0 and 100"));

        var entity = new GradeEntity(
            courseId,
            studentId,
            lessonId,
            examId,
            score,
            assignedBy,
            feedback);

        return Result.Success(entity);
    }
}
