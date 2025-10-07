using CourseModule.Application.Interfaces;
using CourseModule.Domain.Exceptions;
using LessonModule.Domain.Entities;
using LessonModule.Domain.Repositories;
using LessonModule.Domain.ValueObjects.Lessons;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace LessonModule.Application.UseCases.Lessons.Commands;

public record CreateLessonCommand(
    Guid CourseId,
    Theme Theme,
    DateTime Date,
    TimeSpan StartsAt) : ICommand<Unit>;

public class CreateLessonCommandHandler(
    ILessonRepository _lessonRepository,
    IUnitOfWork _unitOfWork,
    ICourseServiceClient _courseService) 
    : ICommandHandler<CreateLessonCommand, Unit>
{
    public async Task<Result<Unit>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseService.IsCourseAvailable(request.CourseId);
        if (course.IsFailure)
            return Results.CustomException<Unit>(course.Error);

        var lesson = LessonEntity.Create(request.CourseId, request.Theme, request.Date, request.StartsAt);

        if (lesson.IsFailure)
            return Result.Failure<Unit>(lesson.Error);

        await _lessonRepository.InsertAsync(lesson.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
