using LessonModule.Domain.Exceptions;
using LessonModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace LessonModule.Application.UseCases.Lessons.Commands;

public record UpdateLessonDateCommand(
    Guid LessonId,
    DateTime Date) : ICommand<Unit>;

public class UpdateLessonDateCommandHandler(
    ILessonRepository _lessonRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<UpdateLessonDateCommand, Unit>
{
    public async Task<Result<Unit>> Handle(UpdateLessonDateCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.SelectByIdAsync(request.LessonId);
        if (lesson is null)
            return Results.NotFoundException<Unit>(LessonErrors.NotFound);

        if (request.Date < DateTime.UtcNow)
            return Results.InvalidArgumentException<Unit>(LessonErrors.InvalidDate);

        lesson.UpdateDate(request.Date);

        await _lessonRepository.UpdateAsync(lesson);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
