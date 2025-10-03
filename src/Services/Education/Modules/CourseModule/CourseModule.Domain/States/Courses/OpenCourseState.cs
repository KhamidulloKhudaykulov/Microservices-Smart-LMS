using CourseModule.Domain.Entitites;
using CourseModule.Domain.Enums;
using CourseModule.Domain.Interfaces;

namespace CourseModule.Domain.States.Courses;

public class OpenCourseState : ICourseStatusState
{
    public Result Block(CourseEntity course)
    {
        course.SetState(new BlockCourseState());
        course.ChangeStatus(CourseStatus.Blocked);

        return Result.Success();
    }

    public Result Close(CourseEntity course)
    {
        course.SetState(new CloseCourseState());
        course.ChangeStatus(CourseStatus.Closed);
        
        return Result.Success();
    }

    public Result Open(CourseEntity course)
    {
        course.SetState(new OpenCourseState());
        course.ChangeStatus(CourseStatus.Opened);
    
        return Result.Success();
    }
}
