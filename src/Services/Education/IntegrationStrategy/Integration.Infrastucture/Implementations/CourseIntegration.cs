using CourseModule.Application.UseCases.Courses.Contracts;
using CourseModule.Domain.Entitites;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using Integration.Logic.Abstractions;
using System.Linq.Expressions;

namespace CourseModule.Infrastructure.Services.Courses;

public class CourseIntegration(
    ICourseRepository _courseRepository)
    : ICourseIntegration
{
    public async Task<Result<CourseResponseDto?>> GetCourse(Guid courseId, Expression<Func<CourseEntity, bool>> predicate)
    {
        var course = await _courseRepository
            .SelectAsync(predicate);

        if (course is null)
            return Results.NotFoundException<CourseResponseDto?>(CourseErrors.NotFound);

        return new CourseResponseDto(course.Id, course.Name, course.StartsAt);
    }

    public async Task<Result<CourseResponseDto?>> GetCourseByIdAsync(Guid courseId)
    {
        var course = await _courseRepository
            .SelectByIdAsync(courseId);

        if (course is null)
            return Results.NotFoundException<CourseResponseDto?>(CourseErrors.NotFound);

        return new CourseResponseDto(course.Id, course.Name, course.StartsAt);
    }

    public async Task<Result<bool>> IsCourseAvailable(Guid courseId)
    {
        var course = await _courseRepository
            .SelectByIdAsync(courseId);

        if (course is null)
            return Results.NotFoundException<bool>(CourseErrors.NotFound);

        return Result.Success(true);
    }

    public async Task<bool> IsStudentExistInCourseAsync(Guid courseId, Guid studentId)
    {
        var course = await _courseRepository
            .SelectByIdAsync(courseId);

        if (course is null)
            return false;

        var hasStudent = course.StudentIds.Contains(studentId);

        return hasStudent;
    }
}
