using CourseModule.Application.Interfaces;
using GradeModule.Application.Common.Rules;
using GradeModule.Application.UseCases.Homeworks.Commands;
using Integration.Logic.Abstractions;
using StudentIntegration.Application.InterfaceBridges;

namespace GradeModule.Application.UseCases.Homeworks.Rules;

public class StudentMustExistRule(
    IStudentServiceClient _studentServiceClient,
    ICourseIntegration _courseIntegration)
    : RuleBase<CreateHomeworkGradeCommand>
{
    protected override async Task<Result> HandleAsync(CreateHomeworkGradeCommand command, CancellationToken cancellationToken)
    {
        var student = await _studentServiceClient
            .VerifyExistStudentById(command.StudentId);

        if (!student)
            return Result.Failure(new Error(
                code: "Student.NotFound",
                message: $"This student with Id={command.StudentId} was not found"));

        if (!await _courseIntegration.IsStudentExistInCourseAsync(command.CourseId, command.StudentId))
            return Result.Failure(new Error(
                code: "Student.CourseError",
                message: $"This student is not attached to this course"));

        return Result.Success();
    }
}
