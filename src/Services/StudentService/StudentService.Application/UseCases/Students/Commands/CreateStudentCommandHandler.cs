using MediatR;
using StudentService.Application.Interfaces.DomainEvents;
using StudentService.Domain.Entities;
using StudentService.Domain.Repositories;

namespace StudentService.Application.UseCases.Students.Commands;

public record CreateStudentCommand(
    string FullName,
    string PhoneNumber,
    string PassportData,
    string Email) : IRequest<Result>;

public sealed class CreateStudentCommandHandler(
    IStudentRepository _studentRepository,
    IDomainEventDispatcher _domainEventDispatcher,
    IUnitOfWork _unitOfWork)
    : IRequestHandler<CreateStudentCommand, Result>
{
    public async Task<Result> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var studentId = Guid.NewGuid();
        var student = Student.Create(
            studentId,
            request.FullName,
            request.PhoneNumber,
            request.PassportData,
            request.Email).Value;

        await _studentRepository.InsertAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _domainEventDispatcher.DispatchAsync(student.DomainEvents);
        student.ClearDomainEvents();

        return Result.Success();
    }
}