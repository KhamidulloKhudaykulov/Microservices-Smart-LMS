using MediatR;
using PaymentService.Application.Interfaces;
using PaymentService.Application.UseCases.Payments.Commands.Rules;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;
using PaymentService.Domain.Repositories;

namespace PaymentService.Application.UseCases.Payments.Commands;

public record CreatePaymentCommand(
    Guid UserId,
    Guid CourseId,
    decimal Amount,
    PaymentMethod PaymentMethod,
    List<Month>? ForMonths) : IRequest<Result<Guid>>;

public class CreatePaymentCommandHandler(
    IPaymentRepository _paymentRepository,
    IUnitOfWork _unitOfWork,
    ICourseServiceClient _courseServiceClient,
    IUserServiceClient _userServiceClient)
    : IRequestHandler<CreatePaymentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var rules = new UserMustExistRule(_userServiceClient)
            .Then(new CourseMustExistRule(_courseServiceClient));

        var validationResult = await rules.CheckAsync(request, cancellationToken);
        if (validationResult.IsFailure)
            return Result.Failure<Guid>(validationResult.Error);

        var payment = PaymentEntity.Create(request.UserId, request.CourseId, request.Amount, DateTime.UtcNow, request.ForMonths, request.PaymentMethod);
        if (payment.IsFailure)
            return Result.Failure<Guid>(payment.Error);

        await _paymentRepository.InsertAsync(payment.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(payment.Value.Id);
    }
}
