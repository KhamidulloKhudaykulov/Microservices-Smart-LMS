using CourseModule.Domain.Entitites;

namespace CourseModule.Domain.Interfaces;

public interface ICourseStatusState
{
    void Open(CourseEntity course);
    void Close(CourseEntity course);
    void Block(CourseEntity course);
}
