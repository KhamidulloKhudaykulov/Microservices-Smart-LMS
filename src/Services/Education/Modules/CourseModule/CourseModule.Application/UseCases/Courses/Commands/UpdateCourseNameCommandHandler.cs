using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace CourseModule.Application.UseCases.Courses.Commands;

public record UpdateCourseNameCommand(
    Guid CourseId,
    string CourseName) : ICommand<Unit>;

public class UpdateCourseNameCommandHandler(
    ICourseRepository _courseRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<UpdateCourseNameCommand, Unit>
{
    public async Task<Result<Unit>> Handle(UpdateCourseNameCommand request, CancellationToken cancellationToken)
    {
        var existCourse = await _courseRepository
            .SelectByIdAsync(request.CourseId);

        if (existCourse is null)
            return Results.NotFoundException<Unit>(CourseErrors.NotFound);

        existCourse.UpdateCourseName(request.CourseName);

        await _courseRepository.UpdateAsync(existCourse);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
