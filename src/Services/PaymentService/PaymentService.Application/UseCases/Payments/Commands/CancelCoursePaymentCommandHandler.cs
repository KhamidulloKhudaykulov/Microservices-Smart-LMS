using MediatR;
using PaymentService.Domain.Repositories;

namespace PaymentService.Application.UseCases.Payments.Commands;

public record CancelCoursePaymentCommand(
    Guid PaymentId,
    string CancellationReason) : IRequest<Result>;

public class CancelCoursePaymentCommandHandler(
    IPaymentRepository _paymentRepository,
    IUnitOfWork _unitOfWork) 
    : IRequestHandler<CancelCoursePaymentCommand, Result>
{
    public async Task<Result> Handle(CancelCoursePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.SelectByIdAsync(request.PaymentId);
        if (payment is null)
            return Result.Failure(new Error(
                code: "Payment.NotFound",
                message: "Payment is not found"));

        payment.Cancel(request.CancellationReason);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}