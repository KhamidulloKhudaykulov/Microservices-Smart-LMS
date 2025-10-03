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
        var existCourse = await _courseRepository
            .SelectByIdAsync(request.CourseId);

        if (existCourse is null)
            return Results.NotFoundException<Unit>(CourseErrors.NotFound);

        var result = existCourse.Close();

        if (result.IsFailure)
            return Results.CustomException<Unit>(result.Error);

        await _courseRepository.UpdateAsync(existCourse);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
