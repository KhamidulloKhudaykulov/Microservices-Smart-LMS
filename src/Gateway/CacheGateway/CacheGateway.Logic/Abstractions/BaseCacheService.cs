using CacheGateway.Core.CacheModels;
using CacheGateway.Core.Enums;
using CacheGateway.Logic.Interfaces;

namespace CacheGateway.Logic.Abstractions;

public abstract class BaseCacheService
{
    protected readonly ICacheService CacheService;

    protected BaseCacheService(ICacheService cacheService)
    {
        CacheService = cacheService;
    }

    protected async Task<T?> GetOrSetAsync<T>(
        string key,
        Func<Task<T>> fetchFunc,
        TimeSpan? expiration = null,
        CacheType cacheType = CacheType.Distributed,
        CacheMetadata? metadata = null)
    {
        var cached = await CacheService.GetAsync<T>(key);
        if (cached != null)
            return cached;

        var freshValue = await fetchFunc();

        if (freshValue != null)
        {
            await CacheService.SetAsync(key, freshValue, expiration ?? TimeSpan.FromMinutes(5), cacheType, metadata);
        }

        return freshValue;
    }

    protected async Task RemoveAsync(string key)
    {
        await CacheService.RemoveAsync(key);
    }

    protected async Task<bool> ExistsAsync(string key)
    {
        return await CacheService.ExistsAsync(key);
    }
}