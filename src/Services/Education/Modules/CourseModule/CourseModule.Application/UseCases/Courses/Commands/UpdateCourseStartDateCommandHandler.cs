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
        var existCourse = await _courseRepository
            .SelectByIdAsync(request.CourseId);

        if (existCourse is null)
            return Results.NotFoundException<Unit>(CourseErrors.NotFound);

        existCourse.UpdateStartDate(request.StartsAt);

        await _courseRepository.UpdateAsync(existCourse);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
