using CourseModule.Domain.Entitites;
using CourseModule.Domain.Enums;
using CourseModule.Domain.Interfaces;

namespace CourseModule.Domain.States.Courses;

public class CloseCourseState : ICourseStatusState
{
    public Result Block(CourseEntity course)
    {
        return Result.Failure(new Error(
            code: "Invalid.State",
            message: "The closed course can't be blocked"));
    }

    public Result Close(CourseEntity course)
    {
        course.SetState(new CloseCourseState());
        course.ChangeStatus(CourseStatus.Closed);

        return Result.Success();
    }

    public Result Open(CourseEntity course)
    {
        return Result.Failure(new Error(
            code: "Invalid.State",
            message: "The closed course can't be opened"));
    }
}
