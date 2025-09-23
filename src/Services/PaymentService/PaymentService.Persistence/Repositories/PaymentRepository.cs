using PaymentService.Domain.Entities;
using PaymentService.Domain.Repositories;
using System.Linq.Expressions;

namespace PaymentService.Persistence.Repositories;

public class PaymentRepository : IPaymentRepository
{
    public Task<PaymentEntity> InsertAsync(PaymentEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PaymentEntity>> SelectAllAsEnumerableAsync(Expression<Func<PaymentEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PaymentEntity> SelectAllAsQueryable()
    {
        throw new NotImplementedException();
    }

    public Task<PaymentEntity?> SelectAsync(Expression<Func<PaymentEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentEntity?> SelectByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentEntity> UpdateAsync(PaymentEntity entity)
    {
        throw new NotImplementedException();
    }
}
