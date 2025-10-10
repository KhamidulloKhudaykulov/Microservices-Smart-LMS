using LessonModule.Application.UseCases.Lessons.Contracts;
using LessonModule.Domain.Exceptions;
using LessonModule.Domain.Repositories;
using SharedKernel.Application.Abstractions.Messaging;

namespace LessonModule.Application.UseCases.Lessons.Queries;

public record GetLessonQuery(
    Guid LessonId) : IQuery<LessonResponseDto>;

public class GetLessonQueryHandler(
    ILessonRepository _lessonRepository)
    : IQueryHandler<GetLessonQuery, LessonResponseDto>
{
    public async Task<Result<LessonResponseDto>> Handle(GetLessonQuery request, CancellationToken cancellationToken)
    {
        var existLesson = await _lessonRepository.SelectByIdAsync(request.LessonId);
        if (existLesson is null)
        {
            return Results.NotFoundException<LessonResponseDto>(LessonErrors.NotFound);
        }

        var result = new LessonResponseDto(
            CourseId: existLesson.CourseId,
            LessonId: existLesson.Id,
            StartsAt: existLesson.StartTime);

        return Result.Success(result);
    }
}
