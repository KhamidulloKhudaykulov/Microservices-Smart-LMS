using Identity.Application.Common;
using Identity.Application.Interfaces;
using Identity.Domain.Repositories;
using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Features.Authentication.Login;

public class LoginCommandHandler(
    IUserRepository _userRepository,
    IPasswordHasher _passwordHasher,
    ITokenService _tokenService) : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var existUser = await _userRepository
            .SelectByUsernameAsync(request.UserName);

        if (existUser == null)
            return Result.Failure<string>(new Error(
                code: "User.NotFound",
                message: $"This user with username {request.UserName} is not found"));

        if (!_passwordHasher.Verify(request.Password, existUser.Password))
            return Result.Failure<string>(new Error(
                code: "User.NotFound",
                message: $"Incorrect username or password"));

        var permissions = existUser.UserRoles
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission.Name)
            .Distinct()
            .ToList();

        var generatedToken = _tokenService.GenerateToken(existUser.UserName, permissions);

        return Result.Success(generatedToken);
    }
}
