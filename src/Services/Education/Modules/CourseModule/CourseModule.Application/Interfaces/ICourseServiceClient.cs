using CourseModule.Application.UseCases.Courses.Contracts;
using CourseModule.Domain.Entitites;
using System.Linq.Expressions;

namespace CourseModule.Application.Interfaces;

public interface ICourseServiceClient
{
    Task<Result<bool>> IsCourseAvailable(Guid courseId);
    Task<Result<CourseResponseDto?>> GetCourseByIdAsync(Guid courseId);
    Task<Result<CourseResponseDto?>> GetCourse(Expression<Func<CourseEntity, bool>> predicate);
    Task<bool> IsStudentExistInCourseAsync(Guid courseId, Guid studentId);
}
