namespace PaymentService.Domain.DomainEvents.Payments;

public class PaymentRefundedDomainEvent
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public decimal Amount { get; set; }
    public DateTime RefundDate { get; set; }
    public List<Enums.Month>? ForMonths { get; set; }
    public PaymentRefundedDomainEvent(
        Guid userId,
        Guid courseId,
        decimal amount,
        DateTime refundDate,
        List<Enums.Month>? forMonths)
    {
        UserId = userId;
        CourseId = courseId;
        Amount = amount;
        RefundDate = refundDate;
        ForMonths = forMonths;
    }
}
