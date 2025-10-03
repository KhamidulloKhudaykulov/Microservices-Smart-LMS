namespace CourseModule.Application.Interfaces;

public interface ICourseService
{
    Task<Result<bool>> IsCourseAvailable(Guid courseId);
}
