using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;
using TeacherService.Domain.Aggregates;
using TeacherService.Domain.Repositories;
using TeacherService.Domain.ValueObjects.Teachers;

namespace TeacherService.Application.UseCases.Teachers.Commands;

public record CreateTeacherCommand(
    Guid Id,
    string Name, 
    string Surname,
    string Email,
    string PhoneNumber)
    : ICommand<Unit>;

public class CreateTeacherCommandHandler(
    ITecherRepository _techerRepository,
    IUnitOfWork _unitOfWork)
    : ICommandHandler<CreateTeacherCommand, Unit>
{
    public async Task<Result<Unit>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var name = TeacherName.Create(request.Name);
        if (name.IsFailure)
            return Result.Failure<Unit>(name.Error);
        var surname = TeacherSurname.Create(request.Surname);
        if (surname.IsFailure)
            return Result.Failure<Unit>(name.Error);
        var email = Email.Create(request.Email);
        if (email.IsFailure)
            return Result.Failure<Unit>(email.Error);
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
        if (phoneNumber.IsFailure)
            return Result.Failure<Unit>(phoneNumber.Error);

        var teacher = Teacher.Create(
            request.Id,
            name.Value,
            surname.Value,
            email.Value,
            phoneNumber.Value);

        if (teacher.IsFailure)
            return Result.Failure<Unit>(teacher.Error);

        await _techerRepository.InsertAsync(teacher.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
