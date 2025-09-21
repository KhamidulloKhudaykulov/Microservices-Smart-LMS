using Identity.Api.Services.Users;

namespace Identity.Api.Infrastructure.Clients.Users;

public class UserServiceClient : IUserServiceClient
{
    private readonly HttpClient _httpClient;

    public UserServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> Verify(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users/auth", new { username, password });
        return response.IsSuccessStatusCode;
    }
}
