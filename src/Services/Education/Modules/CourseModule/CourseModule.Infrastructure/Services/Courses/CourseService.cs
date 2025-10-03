using CourseModule.Application.Interfaces;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;

namespace CourseModule.Infrastructure.Services.Courses;

public class CourseService(
    ICourseRepository _courseRepository) 
    : ICourseService
{
    public async Task<Result<bool>> IsCourseAvailable(Guid courseId)
    {
        var course = await _courseRepository
            .SelectByIdAsync(courseId);

        if (course is null)
            return Results.NotFoundException<bool>(CourseErrors.NotFound);

        return Result.Success(true);
    }
}
