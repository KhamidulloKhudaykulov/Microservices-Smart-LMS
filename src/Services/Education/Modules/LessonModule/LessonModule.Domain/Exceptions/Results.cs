namespace LessonModule.Domain.Exceptions;

public static class Results
{
    public static Result<T> NotFoundException<T>(string message)
    {
        return Result.Failure<T>(new Error(
            code: ErrorType.NotFound,
            message: message));
    }
}
