using Grpc.Core;
using UserService.Domain.Repositories;

namespace UserService.Infrastructure.Grpc.Users.Implementations;

public class UserGrpcServiceHandler(
    IUserRepository _userRepository) : UserService.UserServiceBase
{
    public override async Task<VerifyUserResponse> VerifyExistUser(VerifyUserRequest request, ServerCallContext context)
    {
        var user = await _userRepository.SelectAsync(u => u.Id == Guid.Parse(request.UserId));
        VerifyUserResponse response = new VerifyUserResponse();
        if (user == null)
        {
            response.Exists = false;
            return response;
        }

        response.Exists = true;
        return response;
    }
}
