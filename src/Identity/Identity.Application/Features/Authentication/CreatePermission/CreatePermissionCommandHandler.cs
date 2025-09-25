using Identity.Domain.Repositories;
using Identity.Domain.Shared;
using MediatR;
using Identity.Domain.Entities;

namespace Identity.Application.Features.Authentication.CreatePermission;

public class CreatePermissionCommandHandler(
    IPermissionRepository _permissionRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreatePermissionCommand, Result>
{
    public async Task<Result> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var existPermission = await _permissionRepository
            .SelectByNameAsync(request.PermissionName.ToLower());
        if (existPermission != null)
        {
            return Result.Failure(new Error(
                code: "Permission.AlreadyExists",
                message: $"Permission with name {request.PermissionName} is already exists"));
        }

        var permission = Permission.Create(Guid.NewGuid(), request.PermissionName.ToLower()).Value;
        
        await _permissionRepository.InsertAsync(permission);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
