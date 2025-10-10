using CourseModule.Application.Interfaces;
using CourseModule.Domain.Exceptions;
using LessonModule.Domain.Entities;
using LessonModule.Domain.Repositories;
using LessonModule.Domain.ValueObjects.Lessons;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;

namespace LessonModule.Application.UseCases.Lessons.Commands;

public record CreateLessonCommand(
    Guid CourseId,
    string Theme,
    DateTime Date,
    TimeSpan StartsAt) : ICommand<Unit>;

public class CreateLessonCommandHandler(
    ILessonRepository _lessonRepository,
    ILessonUnitOfWork _unitOfWork,
    ICourseServiceClient _courseService)
    : ICommandHandler<CreateLessonCommand, Unit>
{
    public async Task<Result<Unit>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseService.IsCourseAvailable(request.CourseId);
        if (course.IsFailure)
            return Results.CustomException<Unit>(course.Error);

        var theme = Theme.Create(request.Theme);
        var lesson = LessonEntity.Create(request.CourseId, theme.Value, request.Date, request.StartsAt);

        if (lesson.IsFailure)
            return Result.Failure<Unit>(lesson.Error);

        await _lessonRepository.InsertAsync(lesson.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
