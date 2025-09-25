using Newtonsoft.Json;
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
    public async Task SetAsync<T>(string key, T value)
    {
        var json = JsonConvert.SerializeObject(value);
        await _db.StringSetAsync(key, json);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await _db.StringGetAsync(key);

        if (json.IsNullOrEmpty)
            return default;

        return JsonConvert.DeserializeObject<T>(json!);
    }

    public Task SetExpireAsync(string key, TimeSpan expiration)
    {
        _db.KeyExpire(key, expiration);
        return Task.CompletedTask;
    }

    public async Task RemoveKey(string key)
    {
        await _db.KeyDeleteAsync(key);
    }
}