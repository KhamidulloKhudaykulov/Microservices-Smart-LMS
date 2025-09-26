using Identity.Domain.Repositories;
using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Features.Authentication.SignPermissionToRole;

public class SignPermissionToRoleCommandHandler(
    IPermissionRepository _permissionRepository,
    IRoleRepository _roleRepository,
    IUnitOfWork _unitOfWork)
    : IRequestHandler<SignPermissionToRoleCommand, Result>
{
    public async Task<Result> Handle(SignPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        var existPermission = await _permissionRepository
            .SelectByIdAsync(request.PermissionId);
        if (existPermission is null)
        {
            return Result.Failure(new Error(
                code: "Permission.NotFound",
                message: $"Permission with ID={request.PermissionId} is not found"));
        }

        var existRole = await _roleRepository
           .SelectByIdAsync(request.RoleId);
        if (existRole is null)
        {
            return Result.Failure(new Error(
                code: "Role.NotFound",
                message: $"Role with ID={request.PermissionId} is not found"));
        }

        if (existRole.RolePermissions.Any(x => x.PermissionId == request.PermissionId))
        {
            return Result.Failure(new Error(
                code: "Permission.AlreadyExists",
                message: $"Permission with ID={request.PermissionId} has already signed to this role"));
        }

        existRole.AddPermission(existPermission);

        await _roleRepository.UpdateAsync(existRole);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
