using CourseModule.Domain.Entitites;
using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using SharedKernel.Domain.Repositories;

namespace CourseModule.Application.UseCases.Courses.Commands;

public record CreateCourseCommand(
    Guid AccountId,
    string CourseName,
    DateTime StartsAt) : ICommand<Unit>;

public class CreateCourseCommandHandler(
    ICourseRepository _courseRepository,
    IUnitOfWork _unitOfWork) 
    : ICommandHandler<CreateCourseCommand, Unit>
{
    public async Task<Result<Unit>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.SelectByNameAsync(request.CourseName);
        if (course is not null)
            return Results.AlreadyExistsException<Unit>(CourseErrors.AlreadyExists);

        var newCourse = CourseEntity.Create(
            id: Guid.NewGuid(),
            accountId: request.AccountId,
            name: request.CourseName,
            startsAt: request.StartsAt);

        if (newCourse.IsFailure)
            return Results.CustomException<Unit>(newCourse.Error);

        await _courseRepository.InsertAsync(newCourse.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
