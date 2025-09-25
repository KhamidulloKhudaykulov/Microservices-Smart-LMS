using PaymentService.Application.Common.Rules;
using PaymentService.Application.Interfaces;

namespace PaymentService.Application.UseCases.Payments.Commands.Rules;

public class UserMustExistRule(
    IUserServiceClient _userServiceClient) : RuleBase<CreatePaymentCommand>
{
    protected override async Task<Result> HandleAsync(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        var user = await _userServiceClient.IsUserAvailableAsync(command.UserId);
        if (!user)
            return Result.Failure(new Error(
                "User.NotFound",
                "The specified user does not exist."));


        return Result.Success();
    }
}
