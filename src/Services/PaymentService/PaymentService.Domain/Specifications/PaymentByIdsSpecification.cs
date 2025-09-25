namespace PaymentService.Domain.Specifications;

public class PaymentByIdsSpecification : BaseSpecification
{
    public PaymentByIdsSpecification(Guid accountId, List<Guid> paymentIds)
    {
        if (paymentIds == null || !paymentIds.Any())
            throw new ArgumentException("PaymentIds list cannot be null or empty.", nameof(paymentIds));

        Criteria =
            payment => paymentIds.Contains(payment.Id)
            && payment.AccountId == accountId;

        ApplyOrderByDescending(p => p.PaymentDate);
    }
}
