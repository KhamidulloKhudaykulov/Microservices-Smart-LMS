using HomeworkModule.Domain.Aggregates;
using HomeworkModule.Domain.Repositories;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Data;
using System.Linq.Expressions;

namespace HomeworkModule.Infrastructure.Repositories.Decorators;

public class CachingHomeworkRepository : IHomeworkRepository
{
    private readonly IHomeworkRepository _inner;

    private readonly IDatabase _redisDb;

    public CachingHomeworkRepository(
        IHomeworkRepository inner,
        IConnectionMultiplexer redis)
    {
        _redisDb = redis.GetDatabase();
        _inner = inner;
    }

    private readonly string CacheKeyPrefix = $"hws";

    public async Task DeleteAsync(Homework homework)
        => await _inner.DeleteAsync(homework);
    public async Task<Homework> InsertAsync(Homework homework)
        => await _inner.InsertAsync(homework);

    public IQueryable<Homework> SelectAllAsQueryable(Guid courseId, Expression<Func<Homework, bool>>? predication = null)
        => _inner.SelectAllAsQueryable(courseId, predication);

    public async Task<IEnumerable<Homework>> SelectAllAsync(Guid courseId, Expression<Func<Homework, bool>>? predication = null)
    {
        var entities = await GetAsEnumberableFromRedis(courseId);

        if (!entities.Any())
        {
            var result = await _inner.SelectAllAsync(courseId, predication);

            var cacheKey = GenerateKeyByCourseId(courseId.ToString());
            foreach (var h in result)
            {
                var json = JsonConvert.SerializeObject(h);
                await _redisDb.ListRightPushAsync(cacheKey, json);
                await _redisDb.KeyExpireAsync(cacheKey, TimeSpan.FromMinutes(2));
            }

            return result;
        }

        return predication != null
            ? entities.Where(predication.Compile())
            : entities;
    }

    public async Task<Homework?> SelectAsync(Guid courseId, Expression<Func<Homework, bool>> predicate)
    {
        var entity = (await GetAsEnumberableFromRedis(courseId))
            .FirstOrDefault(predicate.Compile());

        if (entity is not null)
            return entity;

        var result = await _inner.SelectAsync(courseId, predicate);

        var cacheKey = GenerateKeyByCourseId(courseId.ToString());

        var json = JsonConvert.SerializeObject(result);
        await _redisDb.ListRightPushAsync(cacheKey, json);
        await _redisDb.KeyExpireAsync(cacheKey, TimeSpan.FromMinutes(2));

        return result;
    }

    public async Task<Homework?> SelectByIdAsync(Guid courseId, Guid id)
    {
        var entity = await GetAsFromRedis(courseId, id);

        if (entity is not null)
            return entity;

        var result = await _inner.SelectByIdAsync(courseId, id);

        var cacheKey = GenerateKeyByCourseId($"{courseId}:{id}");

        var json = JsonConvert.SerializeObject(result);
        await _redisDb.ListRightPushAsync(cacheKey, json);
        await _redisDb.KeyExpireAsync(cacheKey, TimeSpan.FromMinutes(2));

        return result;
    }
    public async Task<Homework> UpdateAsync(Homework homework)
        => await _inner.UpdateAsync(homework);

    private async Task<IEnumerable<Homework>> GetAsEnumberableFromRedis(Guid courseId)
    {
        var cacheKey = $"hws:{courseId}";
        var cachedData = await _redisDb.ListRangeAsync(cacheKey);

        var entities = cachedData
            .Select(x => JsonConvert.DeserializeObject<Homework>(x))
            .Where(h => h != null)
            .Select(h => h!);

        return entities;
    }

    private async Task<Homework?> GetAsFromRedis(Guid courseId, Guid id)
    {
        var cacheKey = $"hws:{courseId}:{id}";
        var cachedData = await _redisDb.ListRangeAsync(cacheKey);

        var entity = cachedData
            .Select(x => JsonConvert.DeserializeObject<Homework>(x))
            .FirstOrDefault(h => h != null);

        return entity;
    }

    private string GenerateKeyByCourseId(string courseId)
        => $"{CacheKeyPrefix}:{courseId}";
}
