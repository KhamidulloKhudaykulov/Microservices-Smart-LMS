using CourseModule.Application.UseCases.Courses.Helpers;
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
        var result = await CourseRepositoryContract.GetCourseOrNotFoundAsync(_courseRepository, request.CourseId);

        if (result.IsFailure)
            return Results.CustomException<Unit>(result.Error);

        var course = result.Value;

        if (course.StudentIds.Contains(request.StudentId))
            return Results.AlreadyExistsException<Unit>(StudentErrors.AlreadyExists);

        var student = await _studentServiceClient.VerifyExistStudentById(request.StudentId);
        if (!student)
            return Results.NotFoundException<Unit>(StudentErrors.NotFound);

        var addStudentResult = course.AddStudent(request.StudentId);
        if (addStudentResult.IsFailure)
            return Results.CustomException<Unit>(result.Error);

        await _courseRepository.UpdateAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
