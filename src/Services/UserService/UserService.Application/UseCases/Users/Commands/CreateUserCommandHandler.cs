using MediatR;
using UserService.Application.UseCases.Users.Interfaces;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Users.Commands;

public record CreateUserCommand(
    string UserName,
    string Email) : IRequest<Result>;

public sealed class CreateUserCommandHandler(
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork)
    : IRequestHandler<CreateUserCommand, Result>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            Guid.NewGuid(),
            request.UserName,
            request.Email)
            .Value;

        await _userRepository.InsertAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
