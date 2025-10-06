using CourseModule.Domain.Enums;
using CourseModule.Domain.Interfaces;
using CourseModule.Domain.States.Courses;
using SharedKernel.Domain.Primitives;

namespace CourseModule.Domain.Entitites;

public class CourseEntity : AggregateRoot
{
    private CourseEntity(
        Guid id,
        Guid AccountId,
        string name,
        DateTime startsAt) 
        : base(id) 
    {
        Name = name;
        StartsAt = startsAt;
    }
    public Guid AccountId { get; private set; }
    public List<Guid> StudentIds { get; private set; } = new List<Guid>();
    public List<Guid> TeacherIds { get; private set; } = new List<Guid>();
    public string Name { get; private set; }
    public DateTime StartsAt { get; private set; }
    public DateTime? EndsAt { get; private set; }
    public CourseStatus Status { get; protected set; } = CourseStatus.Opened;

    private ICourseStatusState _courseStatusState = new OpenCourseState();

    public static Result<CourseEntity> Create(
        Guid id,
        Guid accountId,
        string name,
        DateTime startsAt)
    {
        var entity = new CourseEntity(id, accountId, name, startsAt);
        return Result.Success(entity);
    }

    public Result AddStudent(Guid studentId)
    {
        StudentIds.Add(studentId);
        return Result.Success();
    }

    public Result AddTeacher(Guid teacherId)
    {
        TeacherIds.Add(teacherId);
        return Result.Success();
    }

    public void UpdateCourseName(string name)
        => Name = name;

    public void UpdateStartDate(DateTime startsAt) 
        => StartsAt = startsAt;

    public void ChangeStatus(CourseStatus status)
        => Status = status;

    public void SetState(ICourseStatusState courseStatusState)
        => _courseStatusState = courseStatusState;

    public Result Open() => _courseStatusState.Open(this);
    public Result Close() => _courseStatusState.Close(this);
    public Result Block() => _courseStatusState.Block(this);
}
