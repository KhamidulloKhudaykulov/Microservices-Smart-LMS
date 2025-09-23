using PaymentService.Application.Common.Rules;
using PaymentService.Application.Interfaces;

namespace PaymentService.Application.UseCases.Payments.Commands.Rules;

public class CourseMustExistRule(
    ICourseServiceClient _courseServiceClient) : RuleBase<CreatePaymentCommand>
{
    protected override async Task<Result> HandleAsync(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        var course = await _courseServiceClient.IsCourseAvailableAsync(command.CourseId);
        if (!course)
            return Result.Failure(new Error(
                "Course.NotFound", 
                "The specified course does not exist."));

        return Result.Success();
    }
}
