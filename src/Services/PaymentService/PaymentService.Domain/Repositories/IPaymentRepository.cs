using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;
using System.Linq.Expressions;

namespace PaymentService.Domain.Repositories;

public interface IPaymentRepository : IRepository<PaymentEntity>
{
}
