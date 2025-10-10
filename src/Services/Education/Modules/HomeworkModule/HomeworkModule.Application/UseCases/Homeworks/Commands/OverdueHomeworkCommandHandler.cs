using HomeworkModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;

namespace HomeworkModule.Application.UseCases.Homeworks.Commands;

public record OverdueHomeworkCommand(
    Guid courseId,
    Guid HomeworkId) : ICommand<Unit>;

public class OverdueHomeworkCommandHandler(
    IHomeworkRepository _homeworkRepository,
    IHomeworkUnitOfWork _unitOfWork)
    : ICommandHandler<OverdueHomeworkCommand, Unit>
{
    public async Task<Result<Unit>> Handle(OverdueHomeworkCommand request, CancellationToken cancellationToken)
    {
        var aggregate = await _homeworkRepository
           .SelectByIdAsync(request.courseId, request.HomeworkId);

        if (aggregate is null)
            return Result.Failure<Unit>(new Error(
                code: "Homework.NotFound",
                message: "This homework was not found"));

        aggregate.Overdue();

        await _homeworkRepository.UpdateAsync(aggregate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
