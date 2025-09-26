using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Features.Authentication.RegisterUser;

public record RegisterUserCommand(string UserName, string Password)
    : IRequest<Result>;
