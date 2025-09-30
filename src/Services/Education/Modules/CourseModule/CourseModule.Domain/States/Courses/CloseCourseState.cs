using CourseModule.Domain.Entitites;
using CourseModule.Domain.Enums;
using CourseModule.Domain.Interfaces;

namespace CourseModule.Domain.States.Courses;

public class CloseCourseState : ICourseStatusState
{
    public void Block(CourseEntity course)
    {
        Result.Failure(new Error(
            code: "Invalid.State",
            message: "The closed course can't be blocked"));
    }

    public void Close(CourseEntity course)
    {
        course.SetState(new CloseCourseState());
        course.ChangeStatus(CourseStatus.Closed);
    }

    public void Open(CourseEntity course)
    {
        Result.Failure(new Error(
            code: "Invalid.State",
            message: "The closed course can't be opened"));
    }
}
