using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Features.Authentication.SignPermissionToRole;

public record SignPermissionToRoleCommand(
    Guid PermissionId, Guid RoleId) : IRequest<Result>;
