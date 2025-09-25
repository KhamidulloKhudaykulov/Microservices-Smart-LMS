using PaymentService.Domain.Enums;
using SharedKernel.Domain.Events;

namespace PaymentService.Domain.DomainEvents.Payments;

public class PaymentCompletedDomainEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public List<Month>? ForMonths { get; set; }
    public PaymentCompletedDomainEvent(
        Guid userId,
        Guid courseId,
        decimal amount,
        DateTime paymentDate,
        List<Month>? forMonths)
    {
        UserId = userId;
        CourseId = courseId;
        Amount = amount;
        PaymentDate = paymentDate;
        ForMonths = forMonths;
    }
}
