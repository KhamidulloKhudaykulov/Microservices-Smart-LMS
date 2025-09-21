using MediatR;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

public class GetUsersQuery : IRequest<Result<IEnumerable<User>>> { }

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<IEnumerable<User>>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.SelectAllAsync();
        return Result.Success(users);
    }
}
