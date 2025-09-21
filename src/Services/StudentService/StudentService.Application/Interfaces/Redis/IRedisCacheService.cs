namespace StudentService.Application.Interfaces.Redis;

public interface IRedisCacheService
{
    Task SetString(string key, string value);
    Task<string> GetString(string key);
    Task RemoveKey(string key);
}