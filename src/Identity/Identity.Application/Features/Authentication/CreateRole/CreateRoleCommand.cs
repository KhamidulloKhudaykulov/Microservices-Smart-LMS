using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Features.Authentication.CreateRole;

public record CreateRoleCommand(
    string RoleName) : IRequest<Result>;
