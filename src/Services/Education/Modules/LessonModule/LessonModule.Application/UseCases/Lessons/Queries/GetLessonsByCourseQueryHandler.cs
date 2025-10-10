using LessonModule.Application.UseCases.Lessons.Contracts;
using LessonModule.Domain.Exceptions;
using LessonModule.Domain.Repositories;
using LessonModule.Domain.Specifications.Lessons;
using SharedKernel.Application.Abstractions.Messaging;

namespace LessonModule.Application.UseCases.Lessons.Queries;

public record GetLessonsByCourseQuery(
    Guid CourseId) : IQuery<List<LessonResponseDto>>;

public class GetLessonsByCourseQueryHandler(
    ILessonRepository _lessonRepository)
    : IQueryHandler<GetLessonsByCourseQuery, List<LessonResponseDto>>
{
    public async Task<Result<List<LessonResponseDto>>> Handle(GetLessonsByCourseQuery request, CancellationToken cancellationToken)
    {
        var lessonSpecification = new LessonByCourseIdSpecification(request.CourseId);

        var lessons = await _lessonRepository.ListAsync(lessonSpecification);
        if (lessons.Count == 0)
            return Results.NotFoundException<List<LessonResponseDto>>(LessonErrors.EmptyList);

        var result = lessons
            .Select(l => new LessonResponseDto(
                l.CourseId,
                l.Id,
                l.StartTime))
            .ToList();

        return Result.Success(result);
    }
}
