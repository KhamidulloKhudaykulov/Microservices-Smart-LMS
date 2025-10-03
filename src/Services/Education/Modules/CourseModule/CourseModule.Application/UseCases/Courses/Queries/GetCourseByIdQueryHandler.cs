using CourseModule.Application.UseCases.Courses.Contracts;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using SharedKernel.Application.Abstractions.Messaging;

namespace CourseModule.Application.UseCases.Courses.Queries;

public record GetCourseByIdQuery(
    Guid CourseId) : IQuery<CourseResponseDto>;

public class GetCourseByIdQueryHandler(
    ICourseRepository _courseRepository) 
    : IQueryHandler<GetCourseByIdQuery, CourseResponseDto>
{
    public async Task<Result<CourseResponseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var existCourse = await _courseRepository
            .SelectByIdAsync(request.CourseId);

        if (existCourse is null)
            return Results.NotFoundException<CourseResponseDto>(
                CourseErrors.NotFound);

        var resposne = new CourseResponseDto(
            Id: existCourse.Id,
            Name: existCourse.Name,
            StartsAt: existCourse.StartsAt);

        return Result.Success(resposne);
    }
}
