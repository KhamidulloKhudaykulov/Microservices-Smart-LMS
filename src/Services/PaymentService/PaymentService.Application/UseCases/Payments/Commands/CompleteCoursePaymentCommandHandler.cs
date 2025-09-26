using MediatR;
using PaymentService.Domain.Repositories;

namespace PaymentService.Application.UseCases.Payments.Commands;

public record CompleteCoursePaymentCommand(
    Guid PaymentId) : IRequest<Result>;

public class CompleteCoursePaymentCommandHandler(
    IPaymentRepository _paymentRepository,
    IUnitOfWork _unitOfWork)
    : IRequestHandler<CompleteCoursePaymentCommand, Result>
{
    public async Task<Result> Handle(CompleteCoursePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.SelectByIdAsync(request.PaymentId);
        if (payment is null)
            return Result.Failure(new Error(
                code: "Payment.NotFound",
                message: "Payment is not found"));

        payment.Complete();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}