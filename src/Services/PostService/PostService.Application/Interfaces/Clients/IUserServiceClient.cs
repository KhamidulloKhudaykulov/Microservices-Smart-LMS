namespace PostService.Application.Interfaces.Clients;

public interface IUserServiceClient
{
    Task<bool> VerifyExistUserAsync(Guid userId);
}
