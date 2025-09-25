using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Domain.Repositories;

public interface IPaymentRepository : IRepository<PaymentEntity>
{
}
