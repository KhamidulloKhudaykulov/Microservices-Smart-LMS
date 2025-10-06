using CourseModule.Application.UseCases.Courses.Helpers;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace CourseModule.Application.UseCases.Courses.Commands;

public record BlockCourseCommand(
    Guid CourseId) : ICommand<Unit>;

public class BlockCourseCommandHandler(
    ICourseRepository _courseRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<BlockCourseCommand, Unit>
{
    public async Task<Result<Unit>> Handle(BlockCourseCommand request, CancellationToken cancellationToken)
    {
        var result = await CourseRepositoryContract.GetCourseOrNotFoundAsync(_courseRepository, request.CourseId);

        if (result.IsFailure)
            return Results.CustomException<Unit>(result.Error);

        var course = result.Value;

        var blockResult = course.Block();

        if (blockResult.IsFailure)
            return Results.CustomException<Unit>(blockResult.Error);

        await _courseRepository.UpdateAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
