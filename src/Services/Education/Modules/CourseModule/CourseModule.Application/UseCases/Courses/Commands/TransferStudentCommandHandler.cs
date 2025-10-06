using CourseModule.Application.UseCases.Courses.Helpers;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace CourseModule.Application.UseCases.Courses.Commands;

public record TransferStudentCommand(
    Guid FromCourseId,
    Guid ToCourseId,
    Guid StudentId) : ICommand<Unit>;

public class TransferStudentCommandHandler(
    ICourseRepository _courseRepository,
    IUnitOfWork _unitOfWork)
    : ICommandHandler<TransferStudentCommand, Unit>
{
    public async Task<Result<Unit>> Handle(TransferStudentCommand request, CancellationToken cancellationToken)
    {
        var from = await CourseRepositoryContract.GetCourseOrNotFoundAsync(_courseRepository, request.FromCourseId);
        if (from.IsFailure) return Results.CustomException<Unit>(from.Error);

        var to = await CourseRepositoryContract.GetCourseOrNotFoundAsync(_courseRepository, request.ToCourseId);
        if (to.IsFailure) return Results.CustomException<Unit>(to.Error);

        if (!from.Value.StudentIds.Remove(request.StudentId))
            return Results.NotFoundException<Unit>(StudentErrors.NotFound);

        to.Value.AddStudent(request.StudentId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
