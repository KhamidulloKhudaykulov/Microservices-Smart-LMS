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
        var course = await _courseRepository
            .SelectByIdAsync(request.CourseId);

        if (course is null)
            return Results.NotFoundException<Unit>(CourseErrors.NotFound);

        var closeResult = course.Close();

        if (closeResult.IsFailure)
            return Results.CustomException<Unit>(closeResult.Error);

        await _courseRepository.UpdateAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
