using PaymentService.Domain.Entities;
using SharedKernel.Domain.Specifications;

namespace PaymentService.Domain.Interfaces;

public interface IPaymentSpecification : ISpecification<PaymentEntity>
{
    Guid AccountId { get; }
}
