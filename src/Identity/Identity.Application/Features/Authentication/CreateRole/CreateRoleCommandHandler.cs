using Identity.Domain.Entities;
using Identity.Domain.Repositories;
using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Features.Authentication.CreateRole;

public class CreateRoleCommandHandler(
    IRoleRepository _roleRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<CreateRoleCommand, Result>
{
    public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var existRole = await _roleRepository
            .SelectByNameAsync(request.RoleName.ToLower());
        if (existRole != null)
        {
            return Result.Failure(new Error(
               code: "Role.AlreadyExists",
               message: $"Role with name {request.RoleName} is already exists"));
        }

        var role = Role.Create(Guid.NewGuid(), request.RoleName).Value;
        
        await _roleRepository.InsertAsync(role);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
