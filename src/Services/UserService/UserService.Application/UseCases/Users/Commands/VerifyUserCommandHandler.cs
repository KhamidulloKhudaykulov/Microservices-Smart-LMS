//using MediatR;
//using UserService.Application.UseCases.Users.Interfaces;
//using UserService.Domain.Repositories;

//namespace UserService.Application.UseCases.Users.Commands;

//public record VerifyUserCommand(
//    string UserName,
//    string Password) : IRequest<Result>;

//public class VerifyUserCommandHandler : IRequestHandler<VerifyUserCommand, Result>
//{
//    private readonly IUserRepository _userRepository;
//    private readonly IPasswordHasher _passwordHasher;

//    public VerifyUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
//    {
//        _userRepository = userRepository;
//        _passwordHasher = passwordHasher;
//    }

//    public async Task<Result> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
//    {
//        var user = await _userRepository.SelectAsync(u => u.UserName == request.UserName);
//        if (user == null)
//            return Result.Failure(new Error(
//                code: "User.NotFound",
//                message: "This user with username is not found"));

//        if (!_passwordHasher.Verify(request.Password, user.Password))
//            return Result.Failure(new Error(
//                code: "Authentication.Failure",
//                message: "Username or password is incorrect"));

//        return Result.Success();
//    }
//}
