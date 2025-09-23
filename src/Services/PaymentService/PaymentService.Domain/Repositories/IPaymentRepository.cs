using PaymentService.Domain.Entities;

namespace PaymentService.Domain.Repositories;

public interface IPaymentRepository
{
    Task<PaymentEntity> InsertAsync(PaymentEntity entity);
    Task<PaymentEntity?> GetByIdAsync(Guid id);
    Task<PaymentEntity> UpdateAsync(PaymentEntity entity);
    IQueryable<PaymentEntity> SelectAllAsQueryable();
}
