namespace StudentService.Application.Interfaces.Redis;

public interface IRedisCacheService
{
    Task SetAsync<T>(string key, T value);
    Task<T?> GetAsync<T>(string key);
    Task RemoveKey(string key);
}