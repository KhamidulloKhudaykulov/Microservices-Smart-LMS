using CourseModule.Application.UseCases.Courses.Contracts;
using CourseModule.Domain.Entitites;
using System.Linq.Expressions;

namespace Integration.Logic.Abstractions;

public interface ICourseIntegration
{
    Task<Result<bool>> IsCourseAvailable(Guid courseId);
    Task<Result<CourseResponseDto?>> GetCourseByIdAsync(Guid courseId);
    Task<Result<CourseResponseDto?>> GetCourse(Guid courseId, Expression<Func<CourseEntity, bool>> predicate);
    Task<bool> IsStudentExistInCourseAsync(Guid courseId, Guid studentId);
}
