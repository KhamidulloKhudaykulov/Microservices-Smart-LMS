using MediatR;
using StudentService.Domain.Entities;
using StudentService.Domain.Repositories;

namespace StudentService.Application.UseCases.Students.Commands;

public record CreateStudentCommand(
    string FullName,
    string PhoneNumber,
    string PassportData) : IRequest<Result>;

public sealed class CreateStudentCommandHandler(
    IStudentRepository _studentRepository,
    IUnitOfWork _unitOfWork)
    : IRequestHandler<CreateStudentCommand, Result>
{
    public async Task<Result> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var studentId = Guid.NewGuid();
        var createResult = Student.Create(
            studentId,
            request.FullName,
            request.PhoneNumber,
            request.PassportData).Value;

        await _studentRepository.InsertAsync(createResult, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}