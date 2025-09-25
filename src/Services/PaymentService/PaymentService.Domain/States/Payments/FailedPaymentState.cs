using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.States.Payments;

public class FailedPaymentState : IPaymentStatusState
{
    public void CancelPayment(PaymentEntity entity, string reason)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot cancel a payment that has failed."));
    }

    public void CompletePayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot complete a payment that has failed."));
    }

    public void CreatePayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot create a payment that has failed."));
    }

    public void FailPayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Failed);
        entity.SetState(this);
    }

    public void ProcessPayment(PaymentEntity entity)
    {
        Result.Failure(new Error(
            "InvalidOperation",
            "Cannot process a payment that has failed."));
    }

    public void RefundPayment(PaymentEntity entity)
    {
        entity.ChangeStatus(Enums.PaymentStatus.Refunded);
        entity.SetState(new RefundedPaymentState());
    }
}
