using CourseModule.Domain.Entitites;
using CourseModule.Domain.Enums;
using CourseModule.Domain.Interfaces;

namespace CourseModule.Domain.States.Courses;

public class OpenCourseState : ICourseStatusState
{
    public void Block(CourseEntity course)
    {
        course.SetState(new BlockCourseState());
        course.ChangeStatus(CourseStatus.Blocked);
    }

    public void Close(CourseEntity course)
    {
        course.SetState(new CloseCourseState());
        course.ChangeStatus(CourseStatus.Closed);
    }

    public void Open(CourseEntity course)
    {
        course.SetState(new OpenCourseState());
        course.ChangeStatus(CourseStatus.Opened);
    }
}
