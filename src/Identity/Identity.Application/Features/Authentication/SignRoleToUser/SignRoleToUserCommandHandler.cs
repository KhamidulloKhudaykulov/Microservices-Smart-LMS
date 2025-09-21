using Identity.Domain.Repositories;
using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Features.Authentication.SignRoleToUser;

public class SignRoleToUserCommandHandler(
    IUserRepository _userRepository,
    IRoleRepository _roleRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<SignRoleToUserCommand, Result>
{
    public async Task<Result> Handle(SignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userRepository
            .SelectByIdAsync(request.UserId);
        if (existUser == null)
        {
            return Result.Failure(new Error(
               code: "User.NotFound",
               message: $"User with ID={request.UserId} is not found"));
        }

        var existRole = await _roleRepository
            .SelectByIdAsync(request.RoleId);
        if (existRole == null)
        {
            return Result.Failure(new Error(
               code: "Role.NotFound",
               message: $"Role with ID={request.RoleId} is not found"));
        }

        existUser.AddRole(existRole);
        await _userRepository.UpdateAsync(existUser, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
