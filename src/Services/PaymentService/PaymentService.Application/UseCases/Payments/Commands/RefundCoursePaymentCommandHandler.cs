using MediatR;
using PaymentService.Domain.Repositories;

namespace PaymentService.Application.UseCases.Payments.Commands;

public record RefundCoursePaymentCommand(
    Guid PayementId) : IRequest<Result>;

public class RefundCoursePaymentCommandHandler(
    IPaymentRepository _paymentRepository,
    IUnitOfWork _unitOfWork) 
    : IRequestHandler<RefundCoursePaymentCommand, Result>
{
    public async Task<Result> Handle(RefundCoursePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.SelectByIdAsync(request.PayementId);
        if (payment is null)
            return Result.Failure(new Error(
                code: "Payment.NotFound",
                message: "Payment is not found"));

        payment.Refund();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
