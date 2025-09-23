using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Domain.States.Payments;

public class RefundedPaymentState : IPaymentStatusState
{
    public void CancelPayment(PaymentEntity entity, string reason)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot cancel a payment that is refunded."));
    }

    public void CompletePayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot complete a payment that is refunded."));
    }

    public void CreatePayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot create a payment that is refunded."));
    }

    public void FailPayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot fail a payment that is refunded."));
    }

    public void ProcessPayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot process a payment that is refunded."));
    }

    public void RefundPayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Refunded);
        entity.SetState(this);
    }
}
