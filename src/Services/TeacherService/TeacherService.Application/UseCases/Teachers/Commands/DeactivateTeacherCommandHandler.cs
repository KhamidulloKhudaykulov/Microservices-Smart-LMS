using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;
using TeacherService.Domain.Repositories;

namespace TeacherService.Application.UseCases.Teachers.Commands;

public record DeactivateTeacherCommand(Guid Id) : ICommand<Unit>;

public class DeactivateTeacherCommandHandler(
    ITecherRepository _teacherRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<DeactivateTeacherCommand, Unit>
{
    public async Task<Result<Unit>> Handle(DeactivateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.SelectByIdAsync(request.Id);
        if (teacher is null)
            return Result.Failure<Unit>(new Error(
                code: "Teacher.NotFound",
                message: $"Teacher with ID={request.Id} was not found"));

        teacher.Deactivate();

        await _teacherRepository.UpdateAsync(teacher);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
