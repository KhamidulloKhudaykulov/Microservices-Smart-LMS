using SharedKernel.Domain.Primitives;

namespace GradeModule.Domain.Enitites;

public class GradeHomeworkEntity : Entity
{
    private GradeHomeworkEntity(
        Guid id,
        Guid studentId,
        Guid homeworkId,
        Guid courseId,
        Guid assignedBy,
        decimal score,
        string? feedback)
    {
        Id = id;
        StudentId = studentId;
        HomeworkId = homeworkId;
        CourseId = courseId;
        Score = score;
        AssignedBy = assignedBy;
        Feedback = feedback;

        AssignedAt = DateTime.UtcNow;
    }
    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid HomeworkId { get; private set; }
    public decimal Score { get; private set; }
    public DateTime AssignedAt { get; private set; }
    public Guid AssignedBy { get; private set; }
    public string? Feedback { get; private set; }

    public static Result<GradeHomeworkEntity> Create(
        Guid id,
        Guid studentId,
        Guid homeworkId,
        Guid courseId,
        Guid assignedBy,
        decimal score,
        string? feedback)
    {
        var result = new GradeHomeworkEntity(
            id,
            studentId,
            homeworkId,
            courseId,
            assignedBy,
            score,
            feedback);

        return Result.Success(result);
    }
}
