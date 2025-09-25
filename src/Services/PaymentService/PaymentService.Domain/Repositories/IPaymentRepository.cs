using PaymentService.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace PaymentService.Domain.Repositories;

public interface IPaymentRepository : IRepository<PaymentEntity>
{
}
