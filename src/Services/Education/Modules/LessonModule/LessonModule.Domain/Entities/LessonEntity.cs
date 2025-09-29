using SharedKernel.Domain.Primitives;

namespace LessonModule.Domain.Entities;

public class LessonEntity : Entity
{
    private LessonEntity(
        Guid courseId,
        string theme,
        DateTime date,
        TimeSpan startTime,
        TimeSpan endTime)
    {
        CourseId = courseId;
        Theme = theme;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
    }
    public Guid CourseId { get; private set; }
    public string Theme { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; } = TimeSpan.Zero;

    public static Result<LessonEntity> Create(
        Guid courseId,
        string theme,
        DateTime date,
        TimeSpan startTime, 
        TimeSpan endTime)
    {
        return Result.Success(new LessonEntity(
            courseId,
            theme,
            date,
            startTime, 
            endTime));
    }
}
