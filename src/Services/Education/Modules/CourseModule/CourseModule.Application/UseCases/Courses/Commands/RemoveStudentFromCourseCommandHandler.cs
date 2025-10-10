using CourseModule.Application.UseCases.Courses.Helpers;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace CourseModule.Application.UseCases.Courses.Commands;

public record RemoveStudentFromCourseCommand(
    Guid CourseId,
    Guid StudentId) : ICommand<Unit>;

public class RemoveStudentFromCourseCommandHandler(
    ICourseRepository _courseRepository,
    IUnitOfWork _unitOfWork)
    : ICommandHandler<RemoveStudentFromCourseCommand, Unit>
{
    public async Task<Result<Unit>> Handle(RemoveStudentFromCourseCommand request, CancellationToken cancellationToken)
    {
        var result = await CourseRepositoryContract.GetCourseOrNotFoundAsync(_courseRepository, request.CourseId);

        if (result.IsFailure)
            return Results.CustomException<Unit>(result.Error);

        var course = result.Value;

        if (!course.StudentIds.Remove(request.StudentId))
            return Results.NotFoundException<Unit>(StudentErrors.NotFound);

        await _courseRepository.UpdateAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
