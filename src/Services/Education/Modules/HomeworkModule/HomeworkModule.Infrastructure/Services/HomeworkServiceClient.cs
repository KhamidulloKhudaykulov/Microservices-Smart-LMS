using HomeworkModule.Application.Interfaces;
using HomeworkModule.Domain.Repositories;

namespace HomeworkModule.Infrastructure.Services;

public class HomeworkServiceClient(
    IHomeworkRepository _homeworkRepository) 
    : IHomeworkServiceClient
{
    public async Task<Result<bool>> CheckExistHomeworkById(Guid courseId, Guid id)
    {
        var entity = await _homeworkRepository
            .SelectByIdAsync(courseId, id);

        if (entity is null)
            return Result.Failure<bool>(new Error(
                code: "Homework.NotFound",
                message: $"This homework with ID={id} was not found"));

        return Result.Success(entity is not null);
    }
}
