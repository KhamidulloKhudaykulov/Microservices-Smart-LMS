using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Features.Authentication.Login;

public record LoginCommand(
    string UserName,
    string Password) : IRequest<Result<string>>;
