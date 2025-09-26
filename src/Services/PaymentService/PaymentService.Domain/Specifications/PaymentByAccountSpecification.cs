namespace PaymentService.Domain.Specifications;

public class PaymentByAccountSpecification : BaseSpecification
{
    public PaymentByAccountSpecification(Guid accountId, int pageNumber, int pageSize)
    {
        AccountId = accountId;
        Criteria = p => p.AccountId == accountId;
        ApplyOrderBy(p => p.PaymentDate);
        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
    }
}
