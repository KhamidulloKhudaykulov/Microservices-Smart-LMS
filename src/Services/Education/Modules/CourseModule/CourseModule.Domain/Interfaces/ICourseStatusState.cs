using CourseModule.Domain.Entitites;

namespace CourseModule.Domain.Interfaces;

public interface ICourseStatusState
{
    Result Open(CourseEntity course);
    Result Close(CourseEntity course);
    Result Block(CourseEntity course);
}
