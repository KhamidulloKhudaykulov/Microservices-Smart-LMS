using HomeworkModule.Application.UseCases.Homeworks.Contracts;
using HomeworkModule.Domain.Repositories;
using SharedKernel.Application.Abstractions.Messaging;

namespace HomeworkModule.Application.UseCases.Homeworks.Queries;

public record GetHomeworksByCourseIdQuery(
    Guid CourseId) : IQuery<List<HomeworkResponseDto>>;

public class GetHomeworksByCourseIdQueryHandler(
    IHomeworkRepository _homeworkRepository)
    : IQueryHandler<GetHomeworksByCourseIdQuery, List<HomeworkResponseDto>>
{
    public async Task<Result<List<HomeworkResponseDto>>> Handle(GetHomeworksByCourseIdQuery request, CancellationToken cancellationToken)
    {
        var aggregates = await _homeworkRepository
            .SelectAllAsync(request.CourseId);

        var result = aggregates
            .Select(h => new HomeworkResponseDto(
                Id: h.Id,
                LessonId: h.LessonId ?? Guid.Empty,
                CreatedBy: h.CreatedBy,
                CreatedAt: h.CreatedDate,
                EndTime: h.EndTime,
                Title: h.Title,
                Description: h.Description,
                MaxScore: h.MaxScore,
                Status: h.Status.ToString()))
            .ToList();

        return Result.Success(result);
    }
}
