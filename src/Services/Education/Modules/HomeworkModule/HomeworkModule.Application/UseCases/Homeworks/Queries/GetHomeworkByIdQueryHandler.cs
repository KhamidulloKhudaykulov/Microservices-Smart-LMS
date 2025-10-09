using HomeworkModule.Application.UseCases.Homeworks.Contracts;
using HomeworkModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;

namespace HomeworkModule.Application.UseCases.Homeworks.Queries;

public record GetHomeworkByIdQuery(
    Guid CourseId,
    Guid HomeworkId) : IQuery<HomeworkResponseDto>;

public class GetHomeworkByIdQueryHandler(
    IHomeworkRepository _homeworkRepository) 
    : IQueryHandler<GetHomeworkByIdQuery, HomeworkResponseDto>
{
    public async Task<Result<HomeworkResponseDto>> Handle(GetHomeworkByIdQuery request, CancellationToken cancellationToken)
    {
        var aggregate = await _homeworkRepository
            .SelectByIdAsync(request.CourseId, request.HomeworkId);

        if (aggregate is null)
            return Result.Failure<HomeworkResponseDto>(new Error(
                code: $"Homework.NotFound",
                message: $"This Homework was not found in this course"));

        return Result.Success(new HomeworkResponseDto(
            aggregate.Id,
            aggregate.LessonId ?? Guid.Empty,
            aggregate.CreatedBy,
            aggregate.CreatedDate,
            aggregate.EndTime,
            aggregate.Title,
            aggregate.Description,
            aggregate.MaxScore,
            aggregate.Status.ToString()));

    }
}
