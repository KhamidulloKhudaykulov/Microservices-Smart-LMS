using Integration.Infrastucture.Decorators;
using Integration.Logic.Abstractions;
using Integration.Logic.Dtos;
using StackExchange.Redis;
using System.Text.Json;

namespace Integration.Infrastucture.Implementations;

public class LessonRedisIntegrationDecorator
    : BaseDecorator<ILessonIntegration>, ILessonIntegration
{
    private readonly IConnectionMultiplexer _redis;

    public LessonRedisIntegrationDecorator(
        ILessonIntegration inner,
        IConnectionMultiplexer redis)
        : base(inner)
    {
        _redis = redis;
    }

    public async Task<bool> ChechExistLessonByIdAsync(Guid lessonId)
    {
        var db = _redis.GetDatabase();
        var cacheKey = $"lesson:{lessonId}:exists";

        if (await db.KeyExistsAsync(cacheKey))
            return true;

        var result = await _inner.ChechExistLessonByIdAsync(lessonId);

        if (result)
            await db.StringSetAsync(cacheKey, "1", TimeSpan.FromMinutes(10));

        return result;
    }

    public async Task<LessonResponseDto?> GetLessonByIdAsync(Guid lessonId)
    {
        var db = _redis.GetDatabase();
        var cacheKey = $"lesson:{lessonId}";

        var cachedValue = await db.StringGetAsync(cacheKey);
        if (cachedValue.HasValue)
        {
            return JsonSerializer.Deserialize<LessonResponseDto>(cachedValue.ToString());
        }

        var dto = await _inner.GetLessonByIdAsync(lessonId);

        if (dto is not null)
        {
            await db.StringSetAsync(
                cacheKey,
                JsonSerializer.Serialize(dto),
                TimeSpan.FromMinutes(10)
            );
        }

        return dto;
    }
}