using CacheGateway.Core.CacheModels;
using CacheGateway.Core.Enums;
using CacheGateway.Infrastructure.Grpc.Client;
using CacheGateway.Logic.Abstractions;
using StackExchange.Redis;
using System.Text.Json;

namespace CacheGateway.Infrastructure.Implementations;

public class StudentCacheService : IStudentCacheService
{
    private readonly StudentGrpcServiceClient _studentGrpcServiceClient;
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;
    private readonly IServer _server;

    public StudentCacheService(StudentGrpcServiceClient studentGrpcServiceClient)
    {
        _studentGrpcServiceClient = studentGrpcServiceClient;

        var options = new ConfigurationOptions
        {
            EndPoints = { "localhost:6379" },
            User = "admin",
            Password = "admin",
            DefaultDatabase = 0,
            AbortOnConnectFail = false
        };

        _redis = ConnectionMultiplexer.Connect(options);
        _db = _redis.GetDatabase(options.DefaultDatabase ?? 0);

        var endpoint = _redis.GetEndPoints().First();
        _server = _redis.GetServer(endpoint);
    }

    public async Task<string?> GetStudentDetailsByIdAsync(Guid id)
    {
        string cacheKey = $"student:{id}";

        return await GetOrSetAsync(
            cacheKey,
            async () => await _studentGrpcServiceClient.GetStudentDetailsById(id),
            TimeSpan.FromMinutes(10),
            CacheType.Distributed,
            new CacheMetadata(
                createdBy: "StudentCacheService",
                source: "StudentService",
                dataType: "json"));
    }

    public async Task<bool> VerifyExistStudentById(Guid id)
    {
        var cacheKey = $"student:exists:{id}";
        var cached = await _db.StringGetAsync(cacheKey);

        if (!cached.IsNullOrEmpty)
            return JsonSerializer.Deserialize<bool>(cached!);

        var result = await _studentGrpcServiceClient.VerifyExistStudentById(id);

        await _db.StringSetAsync(cacheKey, JsonSerializer.Serialize(result), TimeSpan.FromMinutes(5));

        return result;
    }

    private async Task<string?> GetOrSetAsync(
        string cacheKey,
        Func<Task<string?>> factory,
        TimeSpan? expiry = null,
        CacheType cacheType = CacheType.Distributed,
        CacheMetadata? metadata = null)
    {
        var cachedValue = await _db.StringGetAsync(cacheKey);

        if (!cachedValue.IsNullOrEmpty)
            return cachedValue!;

        var result = await factory();

        if (result is not null)
        {
            await _db.StringSetAsync(cacheKey, result, expiry ?? TimeSpan.FromMinutes(10));
        }

        return result;
    }
}
