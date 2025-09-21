using StackExchange.Redis;
using StudentService.Application.Interfaces.Redis;

namespace StudentService.Infrastructure.Utilities.Redis;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _db;

    public RedisCacheService(string connectionString)
    {
        var redis = ConnectionMultiplexer.Connect(connectionString);
        _db = redis.GetDatabase();
    }
    public async Task SetString(string key, string value)
    {
        await _db.StringSetAsync(key, value);
    }

    public async Task<string> GetString(string key)
    {
        return await _db.StringGetAsync(key);
    }

    public async Task RemoveKey(string key)
    {
        await _db.KeyDeleteAsync(key);
    }
}