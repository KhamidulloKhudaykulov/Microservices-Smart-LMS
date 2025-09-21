namespace Identity.Application.Clients;

public interface IUserServiceClient
{
    Task<bool> RegisterAsync(string username);
}
