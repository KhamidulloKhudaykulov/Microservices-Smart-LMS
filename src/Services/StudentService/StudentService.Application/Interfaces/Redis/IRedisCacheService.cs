namespace StudentService.Application.Interfaces.Redis;

public interface IRedisCacheService
{
    Task SetAsync<T>(string key, T value);
    Task<T?> GetAsync<T>(string key);
    Task SetExpireAsync(string key, TimeSpan expiration);
    Task RemoveKey(string key);
}