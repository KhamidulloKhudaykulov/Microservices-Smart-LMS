using CourseModule.Application.Interfaces;
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
    ICourseService _courseService) 
    : ICommandHandler<CreateLessonCommand, Unit>
{
    public async Task<Result<Unit>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseService.IsCourseAvailable(request.CourseId);
        if (!course)
            return Result.Failure<Unit>(new Error(
                code: "Course.NotFound",
                message: $"This course with Id={request.CourseId} wasn't found"));

        var lesson = LessonEntity.Create(request.CourseId, request.Theme, request.Date, request.StartsAt);

        if (lesson.IsFailure)
            return Result.Failure<Unit>(new Error(
                lesson.Error.Code, 
                lesson.Error.Message));

        await _lessonRepository.InsertAsync(lesson.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
