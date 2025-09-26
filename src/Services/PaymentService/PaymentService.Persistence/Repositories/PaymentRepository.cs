using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Repositories;
using SharedKernel.Domain.Specifications;

namespace PaymentService.Persistence.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<PaymentEntity> _dbSet;

    public PaymentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<PaymentEntity>();
    }

    public async Task<int> CountAsync(ISpecification<PaymentEntity> specification)
    {
        var query = ApplySpecification(specification);
        return await query.CountAsync();
    }

    public Task DeleteAsync(PaymentEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentEntity> InsertAsync(PaymentEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<PaymentEntity>> ListAsync(ISpecification<PaymentEntity> specification)
    {
        var query = ApplySpecification(specification);
        return await query.ToListAsync();
    }

    public Task<PaymentEntity?> SelectByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentEntity> UpdateAsync(PaymentEntity entity)
    {
        throw new NotImplementedException();
    }

    private IQueryable<PaymentEntity> ApplySpecification(ISpecification<PaymentEntity> specification)
    {
        return SpecificationEvaluator<PaymentEntity>.GetQuery(_dbSet.AsQueryable(), specification);
    }
}
