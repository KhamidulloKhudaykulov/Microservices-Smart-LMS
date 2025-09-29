namespace CourseModule.Application.Interfaces;

public interface ICourseService
{
    Task<bool> IsCourseAvailable(Guid courseId);
}
