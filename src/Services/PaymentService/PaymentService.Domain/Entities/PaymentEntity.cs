using PaymentService.Domain.Enums;
using PaymentService.Domain.Interfaces;
using PaymentService.Domain.Primitives;
using PaymentService.Domain.States.Payments;

namespace PaymentService.Domain.Entities;

public class PaymentEntity : Entity
{
    private PaymentEntity(
        Guid id,
        Guid userId, 
        Guid courseId, 
        decimal amount, 
        DateTime paymentDate, 
        List<Month>? forMonths, 
        PaymentMethod paymentMethod, 
        PaymentStatus status)
        : base(id)
    {
        UserId = userId;
        CourseId = courseId;
        Amount = amount;
        PaymentDate = paymentDate;
        ForMonths = forMonths;
        PaymentMethod = paymentMethod;
        PaymentStatus = status;
    }

    public Guid UserId { get; private set; }
    public Guid CourseId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public List<Month>? ForMonths { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public string? CancellationReason { get; private set; }
    
    public PaymentStatus PaymentStatus { get; protected set; }
    public IPaymentStatusState _paymentStatusState = new CreatedPaymentState();

    public static Result<PaymentEntity> Create(
        Guid userId, 
        Guid courseId, 
        decimal amount, 
        DateTime paymentDate, 
        List<Month>? forMonths, 
        PaymentMethod paymentMethod, 
        Guid? id = null)
    {
        if (amount <= 0)
        {
            return Result.Failure<PaymentEntity>(new Error("", ""));
        }
        var payment = new PaymentEntity(
            id ?? Guid.NewGuid(),
            userId,
            courseId,
            amount,
            paymentDate,
            forMonths,
            paymentMethod,
            PaymentStatus.Created);

        return Result.Success(payment);
    }

    public void SetState(IPaymentStatusState state)
        => _paymentStatusState = state;

    public void ChangeStatus(PaymentStatus newStatus)
        => PaymentStatus = newStatus;

    public void Complete() => _paymentStatusState.CompletePayment(this);
    public void Process() => _paymentStatusState.ProcessPayment(this);
    public void Refund() => _paymentStatusState.RefundPayment(this);
    public void Cancel(string reason) => _paymentStatusState.CancelPayment(this, reason);
    public void Fail(string reason) => _paymentStatusState.FailPayment(this);
}
