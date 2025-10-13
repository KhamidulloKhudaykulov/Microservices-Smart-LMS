using CourseModule.Application.UseCases.Courses.Contracts;
using CourseModule.Domain.Entitites;
using Integration.Logic.Abstractions;
using StackExchange.Redis;
using System.Linq.Expressions;
using System.Text.Json;

namespace Integration.Infrastucture.Decorators;

public class CourseRedisIntegrationDecorator 
    : BaseDecorator<ICourseIntegration>, ICourseIntegration
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;

    private readonly string KeyPrefix = "courses";

    public CourseRedisIntegrationDecorator(
        ICourseIntegration inner,
        IConnectionMultiplexer redis)
        : base(inner)
    {
        _redis = redis;
        _db = _redis.GetDatabase();
    }

    public async Task<Result<CourseResponseDto>?> GetCourse(Guid courseId, Expression<Func<CourseEntity, bool>> predicate)
    {
        var cacheKey = $"{KeyPrefix}:{courseId}";
        if (await _db.KeyExistsAsync(cacheKey))
        {
            var cachedData = await _db.StringGetAsync(cacheKey);

            var result = JsonSerializer.Deserialize<CourseResponseDto>(cachedData!);

            return Result.Success(result)!;
        }
        
        var response = await _inner.GetCourse(courseId, predicate);
        if (response is null)
            return response;

        var json = JsonSerializer.Serialize(response);
        await _db.StringSetAsync(cacheKey, json, TimeSpan.FromHours(1));

        return response!;
    }

    public async Task<Result<CourseResponseDto>?> GetCourseByIdAsync(Guid courseId)
    {
        var cacheKey = $"{KeyPrefix}:{courseId}";
        if (await _db.KeyExistsAsync(cacheKey))
        {
            var cachedData = await _db.StringGetAsync(cacheKey);

            var result = JsonSerializer.Deserialize<CourseResponseDto>(cachedData!);

            return Result.Success(result)!;
        }

        var response = await _inner.GetCourseByIdAsync(courseId);
        if (response is null)
            return null;

        var json = JsonSerializer.Serialize(response);
        await _db.StringSetAsync(cacheKey, json, TimeSpan.FromHours(1));

        return response!;
    }

    public async Task<Result<bool>> IsCourseAvailable(Guid courseId)
    {
        var cacheKey = $"{KeyPrefix}:{courseId}:exists";
        if (!await _db.KeyExistsAsync(cacheKey))
            return await _inner.IsCourseAvailable(courseId);

        await _db.StringSetAsync(cacheKey, true);
        return Result.Success(true);
    }

    public async Task<bool> IsStudentExistInCourseAsync(Guid courseId, Guid studentId)
    {
        var cacheKey = $"{KeyPrefix}:{courseId}:{studentId}";
        if (!await _db.KeyExistsAsync(cacheKey))
        {
            var cachedData = await _db.StringGetAsync(cacheKey);
            var json = JsonSerializer.Deserialize<bool>(cachedData!);

            return json;
        }

        var result = await _inner.IsStudentExistInCourseAsync(courseId, studentId);

        await _db.StringSetAsync(cacheKey , result);

        return result;
    }
}
