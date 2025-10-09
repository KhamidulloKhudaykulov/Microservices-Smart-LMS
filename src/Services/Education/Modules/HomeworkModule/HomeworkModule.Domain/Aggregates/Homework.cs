using HomeworkModule.Domain.Enums;
using SharedKernel.Domain.Primitives;

namespace HomeworkModule.Domain.Entities;

public class Homework : AggregateRoot
{
    private Homework(
        Guid id,
        string title,
        string? description,
        DateTime endTime,
        Guid lessonId,
        Guid createdBy)
        : base(id)
    {
        Title = title;
        Description = description;
        EndTime = endTime;
        LessonId = lessonId;

        CreatedDate = DateTime.UtcNow;
        Status = HomeworkStatus.InProgress;

        CreatedBy = createdBy;
    }

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public decimal MaxScore { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime EndTime { get; private set; }
    public HomeworkStatus Status { get; private set; }
    public Guid LessonId { get; private set; }

    public Guid CreatedBy { get; private set; }

    public static Result<Homework> Create(
        Guid id,
        string title,
        string? description,
        DateTime endTime,
        Guid lessonId,
        Guid createdBy)
    {
        var result = new Homework(
            id, 
            title, 
            description, 
            endTime, 
            lessonId,
            createdBy);

        return Result.Success(result);
    }

    public Result UpdateEndTime(DateTime endTime)
    {
        EndTime = endTime;
        return Result.Success(this);
    }

    public void Overdue() 
        => Status = HomeworkStatus.Overdue;
}
