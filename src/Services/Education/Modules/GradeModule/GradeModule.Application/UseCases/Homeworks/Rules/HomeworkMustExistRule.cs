using GradeModule.Application.Common.Rules;
using GradeModule.Application.UseCases.Homeworks.Commands;
using HomeworkModule.Application.Interfaces;

namespace GradeModule.Application.UseCases.Homeworks.Rules;

public class HomeworkMustExistRule(
    IHomeworkServiceClient _homeworkServiceClient)
    : RuleBase<CreateHomeworkGradeCommand>
{
    protected override async Task<Result> HandleAsync(CreateHomeworkGradeCommand command, CancellationToken cancellationToken)
    {
        var existHomework = await _homeworkServiceClient
            .CheckExistHomeworkById(command.HomeworkId);

        if (existHomework.IsFailure)
            return Result.Failure(existHomework.Error);

        return Result.Success();
    }
}
