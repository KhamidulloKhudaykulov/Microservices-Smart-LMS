using MediatR;
using StudentService.Application.UseCases.Students.Exceptions;
using StudentService.Domain.Entities;
using StudentService.Domain.Repositories;

namespace StudentService.Application.UseCases.Students.Commands;

public record BlockStudentCommand(
    Guid Id) : IRequest<Result>;

public sealed class BlockStudentCommandHandler(
    IStudentRepository _studentRepository,
    IUnitOfWork _unitOfWork)
    : IRequestHandler<BlockStudentCommand, Result>
{
    public async Task<Result> Handle(BlockStudentCommand request, CancellationToken cancellationToken)
    {
        Student? student = await _studentRepository.SelectAsync(s => s.Id == request.Id);
        if (student == null)
            return StudentBaseException<Student>.StudentNotFoundException();

        student.BlockStudent();
        await _studentRepository.UpdateAsync(student, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}