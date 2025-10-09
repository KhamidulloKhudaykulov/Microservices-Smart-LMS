using CourseModule.Application.Interfaces;
using CourseModule.Application.UseCases.Courses.Contracts;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;

namespace CourseModule.Infrastructure.Services.Courses;

public class CourseService(
    ICourseRepository _courseRepository) 
    : ICourseServiceClient
{
    public async Task<Result<CourseResponseDto>?> GetCourseByIdAsync(Guid courseId)
    {
        var course = await _courseRepository
            .SelectByIdAsync(courseId);

        if (course is null)
            return Results.NotFoundException<CourseResponseDto>(CourseErrors.NotFound);

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
