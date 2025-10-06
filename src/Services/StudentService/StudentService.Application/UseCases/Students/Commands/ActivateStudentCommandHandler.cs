using MediatR;
using StudentService.Application.UseCases.Students.Exceptions;
using StudentService.Domain.Entities;
using StudentService.Domain.Repositories;

namespace StudentService.Application.UseCases.Students.Commands;

public record ActivateStudentCommand(
    Guid id) : IRequest<Result>;

public sealed class ActivateStudentCommandHandler(
    IStudentRepository _studentRepository,
    IUnitOfWork _unitOfWork)
    : IRequestHandler<ActivateStudentCommand, Result>
{
    public async Task<Result> Handle(ActivateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.SelectAsync(u => u.Id == request.id);
        if (student == null)
            return StudentBaseException<Student>.StudentNotFoundException();

        student.ActivateStudent();
        await _studentRepository.InsertAsync(student, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
