using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Domain.States.Payments;

public class CreatePaymentState : IPaymentStatusState
{
    public void CancelPayment(PaymentEntity entity, string reason)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Cancelled);
        entity.SetState(new CanceledPaymentState());
    }

    public void CompletePayment(PaymentEntity entity)
    {
        throw new NotImplementedException();
    }

    public void CreatePayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Created);
        entity.SetState(this);
    }

    public void FailPayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Failed);
        entity.SetState(new FailedPaymentState());
    }

    public void ProcessPayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Processing);
        entity.SetState(new ProcessingPaymentState());
    }

    public void RefundPayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Refunded);
        entity.SetState(new RefundedPaymentState());
    }
}
