using CourseModule.Domain.Enums;
using CourseModule.Domain.Interfaces;
using CourseModule.Domain.States.Courses;
using SharedKernel.Domain.Primitives;

namespace CourseModule.Domain.Entitites;

public class CourseEntity : AggregateRoot
{
    public CourseEntity(
        Guid id,
        string name,
        DateTime startsAt,
        CourseStatus courseStatus) 
        : base(id) 
    {
        Name = name;
        StartsAt = startsAt;
        Status = courseStatus;
    }

    public string Name { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
    public int Duration { get; set; }
    public CourseStatus Status { get; set; }

    private ICourseStatusState _courseStatusState = new OpenCourseState();

    public IList<StudentPayment>? StudentPayments { get; set; }
    public List<Guid>? StudentIds { get; set; }

    public void ChangeStatus(CourseStatus status)
        => Status = status;

    public void SetState(ICourseStatusState courseStatusState)
        => _courseStatusState = courseStatusState;

    public void Open() => _courseStatusState.Open(this);
    public void Close() => _courseStatusState.Close(this);
    public void Block() => _courseStatusState.Block(this);
}
