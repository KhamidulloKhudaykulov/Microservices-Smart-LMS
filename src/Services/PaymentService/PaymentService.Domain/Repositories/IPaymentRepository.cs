using PaymentService.Domain.Entities;
using System.Linq.Expressions;

namespace PaymentService.Domain.Repositories;

public interface IPaymentRepository
{
    Task<PaymentEntity> InsertAsync(PaymentEntity entity);
    Task<PaymentEntity> UpdateAsync(PaymentEntity entity);
    Task<PaymentEntity?> SelectByIdAsync(Guid id);
    Task<PaymentEntity?> SelectAsync(Expression<Func<PaymentEntity, bool>> predicate);
    Task<IEnumerable<PaymentEntity>> SelectAllAsEnumerableAsync(Expression<Func<PaymentEntity, bool>> predicate);
    IQueryable<PaymentEntity> SelectAllAsQueryable();
}
