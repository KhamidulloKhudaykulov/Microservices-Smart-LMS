using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Domain.States.Payments;

public class CanceledPaymentState : IPaymentStatusState
{
    public void CancelPayment(PaymentEntity entity, string reason)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Cancelled);
        entity.SetState(this);
    }

    public void CompletePayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot complete a payment that is cancelled."));
    }

    public void CreatePayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot create a payment that is cancelled."));
    }

    public void FailPayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot fail a payment that is cancelled."));
    }

    public void ProcessPayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot process a payment that is cancelled."));
    }

    public void RefundPayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Refunded);
        entity.SetState(new RefundedPaymentState());
    }
}
