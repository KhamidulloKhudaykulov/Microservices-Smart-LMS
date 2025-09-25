using PaymentService.Domain.Entities;

namespace PaymentService.Domain.Interfaces;

public interface IPaymentSpecification : ISpecification<PaymentEntity>
{
    Guid AccountId { get; }
}
