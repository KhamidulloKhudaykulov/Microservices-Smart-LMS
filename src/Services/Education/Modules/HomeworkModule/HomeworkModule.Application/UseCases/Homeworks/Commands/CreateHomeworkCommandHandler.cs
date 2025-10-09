using HomeworkModule.Domain.Entities;
using HomeworkModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;

namespace HomeworkModule.Application.UseCases.Homeworks.Commands;

public record CreateHomeworkCommand(
    Guid Id,
    Guid LessonId,
    Guid CreatedBy,
    decimal MaxScore,
    DateTime EndTime,
    string Title,
    string? Description) : ICommand<Unit>;

public class CreateHomeworkCommandHandler(
    IHomeworkRepository _homeworkRepository,
    IHomeworkUnitOfWork _unitOfWork)
    : ICommandHandler<CreateHomeworkCommand, Unit>
{
    public async Task<Result<Unit>> Handle(CreateHomeworkCommand request, CancellationToken cancellationToken)
    {
        var aggregate = Homework.Create(
            request.Id, 
            request.Title, 
            request.Description, 
            request.EndTime, 
            request.LessonId, 
            request.CreatedBy);

        if (!aggregate.IsSuccess)
            return Result.Failure<Unit>(aggregate.Error);

        await _homeworkRepository.InsertAsync(aggregate.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
