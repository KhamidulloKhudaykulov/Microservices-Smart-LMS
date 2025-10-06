using CourseModule.Application.UseCases.Courses.Contracts;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;

namespace CourseModule.Application.UseCases.Courses.Queries;

public record GetStudentsByCourseIdQuery(
    Guid CourseId) : IQuery<CourseStudentResponseDto>;

public class GetStudentsByCourseIdQueryHandler(
    ICourseRepository _courseRepository)
    : IQueryHandler<GetStudentsByCourseIdQuery, CourseStudentResponseDto>
{
    public async Task<Result<CourseStudentResponseDto>> Handle(GetStudentsByCourseIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.SelectByIdAsync(request.CourseId);
        if (course is null)
            return Results.NotFoundException<CourseStudentResponseDto>(CourseErrors.NotFound);

        var result = new CourseStudentResponseDto(StudentIds: course.StudentIds);
        return Result.Success(result);
    }
}
