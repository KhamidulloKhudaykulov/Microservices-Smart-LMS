using Identity.Application.Clients;
using Identity.Application.Common;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Features.Authentication.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork,
    IPasswordHasher _passwordHasher,
    IUserServiceClient _userServiceClient) : IRequestHandler<RegisterUserCommand, Result>
{
    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userRepository.SelectByUsernameAsync(request.UserName);
        if (existUser != null)
        {
            return Result.Failure(new Error(
                code: "User.AlreadyExists",
                message: $"{request.UserName} was registered."));
        }

        var newUser = User.Create(
            Guid.NewGuid(),
            request.UserName, 
            _passwordHasher.Generate(request.Password)).Value;

        if (!await _userServiceClient.RegisterAsync(newUser.UserName))
        {
            return Result.Failure(new Error(
                code: "Server.Error",
                message: "Internal Server Error"));
        }

        await _userRepository.InsertAsync(newUser);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // After this action should be add POST method for UserService

        return Result.Success();
    }
}
