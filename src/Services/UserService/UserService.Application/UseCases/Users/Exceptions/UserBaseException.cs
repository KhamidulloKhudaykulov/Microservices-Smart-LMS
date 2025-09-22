using UserService.Domain.Primitives;

namespace UserService.Application.UseCases.Users.Exceptions;

public static class UserBaseException<T> where T : Entity
{
    public static Result<T> UserNotFoundException()
        => Result.Failure<T>(new Error(
            code: "User.NotFound",
            message: "This user is not found"));

    public static Result<T> UserAlreadyExistException()
        => Result.Failure<T>(new Error(
            code: "User.NotFound",
            message: "This user is not found"));
}
