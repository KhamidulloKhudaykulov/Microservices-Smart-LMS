using Newtonsoft.Json;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Repositories;
using StackExchange.Redis;
using System.Linq.Expressions;

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

    public Task<IEnumerable<PaymentEntity>> SelectAllAsEnumerableAsync(Expression<Func<PaymentEntity, bool>> predicate)
    {
        var dataFromCache = _redisDb.StringGet($"{CachePrefix}");
        if (dataFromCache.HasValue)
        {
            var cachedEntities = JsonConvert.DeserializeObject<IEnumerable<PaymentEntity>>(dataFromCache!);
            if (cachedEntities != null)
            {
                return Task.FromResult(cachedEntities);
            }
        }

        var entities = _inner.SelectAllAsEnumerableAsync(predicate);
        var serializedData = JsonConvert.SerializeObject(entities.Result);
        _redisDb.StringSet($"{CachePrefix}", serializedData, TimeSpan.FromMinutes(10));
        
        return entities;
    }

    public IQueryable<PaymentEntity> SelectAllAsQueryable()
    {
        return _inner.SelectAllAsQueryable();
    }

    public Task<PaymentEntity?> SelectAsync(Expression<Func<PaymentEntity, bool>> predicate)
    {
        return _inner.SelectAsync(predicate);
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
}
