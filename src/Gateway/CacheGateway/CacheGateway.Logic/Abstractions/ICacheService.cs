using CacheGateway.Core.CacheModels;
using CacheGateway.Core.Enums;

namespace CacheGateway.Logic.Interfaces;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null,
        CacheType cacheType = CacheType.Distributed, CacheMetadata? metadata = null);
    Task RemoveAsync(string key);
    Task<bool> ExistsAsync(string key);
}