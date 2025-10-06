using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;
using StudentIntegration.Application.InterfaceBridges;

namespace CourseModule.Application.UseCases.Courses.Commands;

public record AttachStudentCommand(
    Guid CourseId,
    Guid StudentId) : ICommand<Unit>;

public class AttachStudentCommandHandler(
    ICourseRepository _courseRepository,
    IUnitOfWork _unitOfWork,
    IStudentServiceClient _studentServiceClient)
    : ICommandHandler<AttachStudentCommand, Unit>
{
    public async Task<Result<Unit>> Handle(AttachStudentCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.SelectByIdAsync(request.CourseId);
        if (course is null)
            return Results.NotFoundException<Unit>(CourseErrors.NotFound);

        if (course.StudentIds.Contains(request.StudentId))
            return Results.AlreadyExistsException<Unit>(StudentErrors.AlreadyExists);

        var student = await _studentServiceClient.VerifyExistStudentById(request.StudentId);
        if (student is null)
            return Results.NotFoundException<Unit>(StudentErrors.NotFound);

        var result = course.AddStudent(request.StudentId);
        if (result.IsFailure)
            return Results.CustomException<Unit>(result.Error);

        await _courseRepository.UpdateAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
