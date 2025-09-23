using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Domain.States.Payments;

public class ProcessingPaymentState : IPaymentStatusState
{
    public void CancelPayment(PaymentEntity entity, string reason)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Cancelled);
        entity.SetState(new CanceledPaymentState());
    }

    public void CompletePayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Completed);
        entity.SetState(new CompletedPaymentState());
    }

    public void CreatePayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot create a payment that is processing."));
    }

    public void FailPayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Failed);
        entity.SetState(new FailedPaymentState());
    }

    public void ProcessPayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Processing);
        entity.SetState(this);
    }

    public void RefundPayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Refunded);
        entity.SetState(new RefundedPaymentState());
    }
}
