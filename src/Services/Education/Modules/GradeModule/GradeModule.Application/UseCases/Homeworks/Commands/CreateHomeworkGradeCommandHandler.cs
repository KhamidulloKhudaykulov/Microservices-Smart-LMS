using CourseModule.Application.Interfaces;
using GradeModule.Application.UseCases.Homeworks.Rules;
using GradeModule.Domain.Enitites;
using GradeModule.Domain.Repositories;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using StudentIntegration.Application.InterfaceBridges;

namespace GradeModule.Application.UseCases.Homeworks.Commands;

public record CreateHomeworkGradeCommand(
    Guid Id,
    Guid CourseId,
    Guid HomeworkId,
    Guid StudentId,
    Guid AssignedBy,
    decimal Score,
    string? Feedback) : ICommand<Unit>;

public class CreateHomeworkGradeCommandHandler(
    IGradeHomeworkRepository _gradeHomeworkRepository,
    IGradeUnitOfWork _unitOfWork,
    IStudentServiceClient _studentServiceClient,
    ICourseServiceClient _courseServiceClient) 
    : ICommandHandler<CreateHomeworkGradeCommand, Unit>
{
    public async Task<Result<Unit>> Handle(CreateHomeworkGradeCommand request, CancellationToken cancellationToken)
    {
        var rules = new StudentMustExistRule(_studentServiceClient, _courseServiceClient);

        var validationResult = await rules.CheckAsync(request, cancellationToken);
        if (validationResult.IsFailure)
            return Result.Failure<Unit>(validationResult.Error);

        var entity = GradeHomeworkEntity.Create(
            request.Id,
            request.StudentId,
            request.HomeworkId,
            request.AssignedBy,
            request.Score,
            request.Feedback);

        if (entity.IsFailure)
            return Result.Failure<Unit>(entity.Error);

        await _gradeHomeworkRepository.InsertAsync(entity.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
