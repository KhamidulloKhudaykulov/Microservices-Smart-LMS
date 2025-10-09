using HomeworkModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;

namespace HomeworkModule.Application.UseCases.Homeworks.Commands;

public record UpdateHomeworkEndTimeCommand(
    Guid CourseId,
    Guid HomeworkId,
    DateTime EndTime) : ICommand<Unit>;

public class UpdateHomeworkEndTimeCommandHandler(
    IHomeworkRepository _homeworkRepository,
    IHomeworkUnitOfWork _unitOfWork) 
    : ICommandHandler<UpdateHomeworkEndTimeCommand, Unit>
{
    public async Task<Result<Unit>> Handle(UpdateHomeworkEndTimeCommand request, CancellationToken cancellationToken)
    {
        var aggregate = await _homeworkRepository
            .SelectByIdAsync(request.CourseId, request.HomeworkId);

        if (aggregate is null)
            return Result.Failure<Unit>(new Error(
                code: "Homework.NotFound",
                message: "This homework was not found"));

        aggregate.UpdateEndTime(request.EndTime);

        await _homeworkRepository.UpdateAsync(aggregate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
