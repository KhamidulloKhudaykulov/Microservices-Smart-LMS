using Grpc.Net.Client;
using PostService.Application.Interfaces.Clients;

namespace PostService.Infrastructure.Grpc.Users.Services;

public class UserGrpcClientService : IUserServiceClient
{
    private readonly UserService.UserServiceClient _client;

    public UserGrpcClientService()
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7226");
        _client = new UserService.UserServiceClient(channel);
    }

    public async Task<bool> VerifyExistUserAsync(Guid userId)
    {
        var request = new VerifyUserRequest { UserId = userId.ToString() };

        var response = await _client.VerifyExistUserAsync(request);

        return response.Exists;
    }
}