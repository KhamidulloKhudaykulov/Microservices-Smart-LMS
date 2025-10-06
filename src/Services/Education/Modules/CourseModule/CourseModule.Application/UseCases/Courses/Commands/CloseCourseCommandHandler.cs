using CourseModule.Application.UseCases.Courses.Helpers;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace CourseModule.Application.UseCases.Courses.Commands;

public record CloseCourseCommand(
    Guid CourseId) : ICommand<Unit>;

public class CloseCourseCommandHandler(
    ICourseRepository _courseRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<CloseCourseCommand, Unit>
{
    public async Task<Result<Unit>> Handle(CloseCourseCommand request, CancellationToken cancellationToken)
    {
        var result = await CourseRepositoryContract.GetCourseOrNotFoundAsync(_courseRepository, request.CourseId);

        if (result.IsFailure)
            return Results.CustomException<Unit>(result.Error);

        var course = result.Value;

        var closeResult = course.Close();

        if (closeResult.IsFailure)
            return Results.CustomException<Unit>(closeResult.Error);

        await _courseRepository.UpdateAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
