using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Features.Authentication.CreatePermission;

public record CreatePermissionCommand(
    string PermissionName) : IRequest<Result>;
