using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;
using TeacherService.Domain.Repositories;

namespace TeacherService.Application.UseCases.Teachers.Commands;

public record UpdateTeacherCommand(
    Guid Id,
    string? Name,
    string? Surname,
    string? Email,
    string? PhoneNumber) : ICommand<Unit>;

public class UpdateTeacherCommandHandler(
    ITecherRepository _techerRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<UpdateTeacherCommand, Unit>
{
    public async Task<Result<Unit>> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {
        var existTeacher = await _techerRepository
            .SelectByIdAsync(request.Id);
        if (existTeacher is null)
            return Result.Failure<Unit>(new Error(
                code: "Teacher.NotFound",
                message: $"This teacher with ID={request.Id} was not found"));

        var updateResult = existTeacher.Update(
            request.Name,
            request.Surname, 
            request.Email, 
            request.PhoneNumber);

        if (updateResult.IsFailure)
            return Result.Failure<Unit>(updateResult.Error);

        await _techerRepository.UpdateAsync(existTeacher);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
