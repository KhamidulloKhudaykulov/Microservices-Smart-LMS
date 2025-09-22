using StudentService.Domain.Interfaces;
using StudentService.Domain.Primitives;

namespace StudentService.Application.UseCases.Students.Exceptions;

public static class StudentBaseException<T> where T : Entity
{
    public static Result<T> StudentNotFoundException()
        => Result.Failure<T>(new Error(
            code: "Student.NotFound",
            message: "This student is not found"));

    public static Result<T> StudentAlreadyExistException()
        => Result.Failure<T>(new Error(
            code: "Student.AlreadyExist",
            message: "This student is not found"));
}
