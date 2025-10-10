using CourseModule.Application.UseCases.Courses.Helpers;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace CourseModule.Application.UseCases.Courses.Commands;

public record UpdateCourseStartDateCommand(
    Guid CourseId,
    DateTime StartsAt) : ICommand<Unit>;

public class UpdateCourseStartDateCommandHandler(
    ICourseRepository _courseRepository,
    IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateCourseStartDateCommand, Unit>
{
    public async Task<Result<Unit>> Handle(UpdateCourseStartDateCommand request, CancellationToken cancellationToken)
    {
        var result = await CourseRepositoryContract.GetCourseOrNotFoundAsync(_courseRepository, request.CourseId);

        if (result.IsFailure)
            return Results.CustomException<Unit>(result.Error);

        var course = result.Value;

        course.UpdateStartDate(request.StartsAt);

        await _courseRepository.UpdateAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
