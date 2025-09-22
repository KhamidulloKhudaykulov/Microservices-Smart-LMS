using PaymentService.Domain.Entities;

namespace PaymentService.Domain.Interfaces;

public interface IPaymentStatusState
{
    void CreatePayment(PaymentEntity entity);
    void ProcessPayment(PaymentEntity entity);
    void CompletePayment(PaymentEntity entity);
    void CancelPayment(PaymentEntity entity, string reason);
    void RefundPayment(PaymentEntity entity);
    void FailPayment(PaymentEntity entity);
}
