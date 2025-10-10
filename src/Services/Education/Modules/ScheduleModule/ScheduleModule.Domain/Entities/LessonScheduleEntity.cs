using SharedKernel.Domain.Primitives;

namespace ScheduleModule.Domain.Entities;

public class LessonScheduleEntity : Entity
{
    private LessonScheduleEntity(
        Guid lessonId)
    {
        LessonId = lessonId;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid LessonId { get; private set; }
    public List<Guid> AbsentStudents { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }

    public static Result<LessonScheduleEntity> Create(Guid lessonId)
    {
        return Result.Success(new LessonScheduleEntity(lessonId));
    }

    public void MarkStudentsAbsent(List<Guid> studentIds)
    {
        foreach (var student in studentIds)
            if (!AbsentStudents.Contains(student))
                AbsentStudents.Add(student);
    }

    public void MarkStudentsPresent(List<Guid> studentIds)
    {
        foreach (var student in studentIds)
            AbsentStudents.Remove(student);
    }
}
