using Newtonsoft.Json;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;
using PaymentService.Domain.Repositories;
using PaymentService.Infrastructure.Heplers.Redis;
using SharedKernel.Domain.Specifications;
using StackExchange.Redis;

namespace StudentService.Infrastructure.Decorators.Repositories;

public class CachingPaymentRepository : IPaymentRepository
{
    private readonly IPaymentRepository _inner;
    private readonly IDatabase _redisDb;

    public CachingPaymentRepository(IConnectionMultiplexer redis, IPaymentRepository inner)
    {
        _redisDb = redis.GetDatabase();
        _inner = inner;
    }

    private static readonly string CachePrefix = "payments";

    public async Task<PaymentEntity> InsertAsync(PaymentEntity entity)
    {
        var result = await _inner.InsertAsync(entity);

        var cacheKey = $"{CachePrefix}:{entity.Id}:{entity.UserId}";
        await _redisDb.KeyDeleteAsync(cacheKey);

        return result;
    }

    public async Task<PaymentEntity?> SelectByIdAsync(Guid id)
    {
        var dataFromCache = _redisDb.StringGet($"{CachePrefix}:payments:{id}");
        if (dataFromCache.HasValue)
        {
            var cachedEntity = JsonConvert.DeserializeObject<PaymentEntity>(dataFromCache!);
            return await Task.FromResult(cachedEntity);
        }

        return await _inner.SelectByIdAsync(id);
    }

    public async Task<PaymentEntity> UpdateAsync(PaymentEntity entity)
    {
        return await _inner.UpdateAsync(entity);
    }

    public Task DeleteAsync(PaymentEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<PaymentEntity>> ListAsync(ISpecification<PaymentEntity> specification)
    {
        var paymentSpecification = specification as IPaymentSpecification;
        if (paymentSpecification == null)
            throw new ArgumentException("Specification must be of type IPaymentSpecification", nameof(specification));

        var cacheKey = RedisKeyHelper.GeneratePaymentKey(paymentSpecification.AccountId.ToString(), specification.Take, specification.Skip);
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(ISpecification<PaymentEntity> specification)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentEntity> SelectAsync(ISpecification<PaymentEntity> specification)
    {
        throw new NotImplementedException();
    }
}
