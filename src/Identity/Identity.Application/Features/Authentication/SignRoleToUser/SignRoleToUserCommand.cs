using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Features.Authentication.SignRoleToUser;

public record SignRoleToUserCommand(
    Guid UserId,
    Guid RoleId) : IRequest<Result>;
