using CourseModule.Domain.Entitites;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using MediatR;

namespace CourseModule.Application.UseCases.Courses.Helpers;

public static class CourseRepositoryContract
{
    public static async Task<Result<CourseEntity>> GetCourseOrNotFoundAsync(
        ICourseRepository _courseRepository,
        Guid courseId)
    {
        var course = await _courseRepository
            .SelectByIdAsync(courseId);

        if (course is null)
            return Results.NotFoundException<CourseEntity>(CourseErrors.NotFound);

        return course;
    }
}
