using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Domain.States.Payments;

public class CompletedPaymentState : IPaymentStatusState
{
    public void CancelPayment(PaymentEntity entity, string reason)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot cancel a payment that is completed."));
    }

    public void CompletePayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Completed);
        entity.SetState(this);
    }

    public void CreatePayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot create a payment that is completed."));
    }

    public void FailPayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot fail a payment that is completed."));
    }

    public void ProcessPayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot process a payment that is completed."));
    }

    public void RefundPayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Refunded);
        entity.SetState(new RefundedPaymentState());
    }
}
