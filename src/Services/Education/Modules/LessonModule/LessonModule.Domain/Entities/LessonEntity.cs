using LessonModule.Domain.ValueObjects.Lessons;
using SharedKernel.Domain.Primitives;

namespace LessonModule.Domain.Entities;

public class LessonEntity : Entity
{
    protected LessonEntity() { }
    private LessonEntity(
        Guid courseId,
        Theme theme,
        DateTime date,
        TimeSpan startTime,
        TimeSpan? endTime)
    {
        CourseId = courseId;
        Theme = theme;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
    }
    public Guid CourseId { get; private set; }
    public Theme Theme { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan? EndTime { get; private set; }

    public static Result<LessonEntity> Create(
        Guid courseId,
        Theme theme,
        DateTime date,
        TimeSpan startTime, 
        TimeSpan? endTime = null)
    {
        return Result.Success(new LessonEntity(
            courseId,
            theme,
            date,
            startTime, 
            endTime));
    }

    public void UpdateDate(DateTime date)
    {
        Date = date;
        StartTime = date.TimeOfDay;
    }
}
