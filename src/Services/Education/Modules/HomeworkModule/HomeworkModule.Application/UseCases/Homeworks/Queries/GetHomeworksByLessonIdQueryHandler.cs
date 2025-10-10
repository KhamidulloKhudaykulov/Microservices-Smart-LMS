using HomeworkModule.Application.UseCases.Homeworks.Contracts;
using HomeworkModule.Domain.Repositories;
using SharedKernel.Application.Abstractions.Messaging;

namespace HomeworkModule.Application.UseCases.Homeworks.Queries;

public record GetHomeworksByLessonIdQuery(
    Guid CourseId,
    Guid LessonId) : IQuery<List<HomeworkResponseDto>>;

public class GetHomeworksByLessonIdQueryHandler(
    IHomeworkRepository _homeworkRepository)
    : IQueryHandler<GetHomeworksByLessonIdQuery, List<HomeworkResponseDto>>
{
    public async Task<Result<List<HomeworkResponseDto>>> Handle(GetHomeworksByLessonIdQuery request, CancellationToken cancellationToken)
    {
        var aggregates = await _homeworkRepository
            .SelectAllAsync(request.CourseId, h => h.LessonId == request.LessonId);

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
