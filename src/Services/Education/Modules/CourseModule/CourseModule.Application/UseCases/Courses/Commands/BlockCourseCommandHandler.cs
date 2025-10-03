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
        var course = await _courseRepository
            .SelectByIdAsync(request.CourseId);

        if (course is null)
            return Results.NotFoundException<Unit>(CourseErrors.NotFound);

        var blockResult = course.Block();

        if (blockResult.IsFailure)
            return Results.CustomException<Unit>(blockResult.Error);

        await _courseRepository.UpdateAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
