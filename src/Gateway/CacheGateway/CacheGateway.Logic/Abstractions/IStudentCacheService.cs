namespace CacheGateway.Logic.Abstractions;

public interface IStudentCacheService
{
    public Task<string?> GetStudentDetailsByIdAsync(Guid id);
    public Task<bool> VerifyExistStudentById(Guid id);
}
