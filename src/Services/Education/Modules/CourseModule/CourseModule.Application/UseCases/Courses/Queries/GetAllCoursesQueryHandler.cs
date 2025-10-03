using CourseModule.Application.UseCases.Courses.Contracts;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using SharedKernel.Application.Abstractions.Messaging;

namespace CourseModule.Application.UseCases.Courses.Queries;

public record GetAllCoursesQuery(
    Guid accountId) : IQuery<IEnumerable<CourseResponseDto>>;

public class GetAllCoursesQueryHandler(
    ICourseRepository _courseRepository) 
    : IQueryHandler<GetAllCoursesQuery, IEnumerable<CourseResponseDto>>
{
    public async Task<Result<IEnumerable<CourseResponseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository
            .SelectAllByAccountIdAsync(request.accountId);

        if (courses is null || !courses.Any())
            return Results.NotFoundException<IEnumerable<CourseResponseDto>>(
                CourseErrors.EmptyList);

        var response = courses
            .Select(c => new CourseResponseDto(
                Id: c.Id,
                Name: c.Name,
                StartsAt: c.StartsAt));

        return Result.Success(response);
    }
}
