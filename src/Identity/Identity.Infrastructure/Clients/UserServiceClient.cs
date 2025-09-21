using Identity.Application.Clients;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Identity.Infrastructure.Clients;

public class UserServiceClient : IUserServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly JwtService _jwtService;

    public UserServiceClient(HttpClient httpClient, JwtService tokenService)
    {
        _httpClient = httpClient;
        _jwtService = tokenService;
    }

    public async Task<bool> RegisterAsync(string username)
    {
        var serviceToken = _jwtService.GenerateTokenForOtherService();

        var request = new HttpRequestMessage(HttpMethod.Post, "api/users")
        {
            Content = JsonContent.Create(new { username })
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", serviceToken);

        var response = await _httpClient.SendAsync(request);

        return response.IsSuccessStatusCode;
    }
}
