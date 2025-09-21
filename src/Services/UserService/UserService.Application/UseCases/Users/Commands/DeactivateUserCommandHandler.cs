using MediatR;
using UserService.Application.UseCases.Users.Exceptions;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Application.UseCases.Users.Commands;

public record DeactivateUserCommand(
    Guid id) : IRequest<Result>;

public sealed class DeactivateUserCommandHandler(
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork)
    : IRequestHandler<ActivateUserCommand, Result>
{
    public async Task<Result> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.SelectAsync(u => u.Id == request.id);
        if (user == null)
            return UserBaseException<User>.UserNotFoundException();

        user.DeactivateUser();
        await _userRepository.InsertAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
