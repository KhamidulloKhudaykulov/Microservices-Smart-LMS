namespace LessonModule.Domain.Exceptions;

public static class Results
{
    public static Result<T> CustomException<T>(Error error)
        => Result.Failure<T>(error);

    public static Result<T> NotFoundException<T>(string message)
    {
        return Result.Failure<T>(new Error(
            code: ErrorType.NotFound,
            message: message));
    }

    public static Result<T> InvalidArgumentException<T>(string message)
    {
        return Result.Failure<T>(new Error(
            code: ErrorType.InvalidArgument,
            message: message));
    }
}
