using PaymentService.Domain.Entities;

namespace PaymentService.Domain.Specifications;

public class PaymentByAccountSpecification : BaseSpecification<PaymentEntity>
{
    public PaymentByAccountSpecification(Guid accountId, int pageNumber, int pageSize)
    {
        Criteria = p => p.AccountId == accountId;
        ApplyOrderBy(p => p.PaymentDate);
        ApplyPaging((pageNumber - 1) * pageSize, pageSize);
    }
}
