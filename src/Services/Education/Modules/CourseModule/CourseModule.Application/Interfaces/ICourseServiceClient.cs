using CourseModule.Application.UseCases.Courses.Contracts;

namespace CourseModule.Application.Interfaces;

public interface ICourseServiceClient
{
    Task<Result<bool>> IsCourseAvailable(Guid courseId);
    Task<Result<CourseResponseDto>?> GetCourseByIdAsync(Guid courseId);
    Task<bool> IsStudentExistInCourseAsync(Guid courseId, Guid studentId);
}
