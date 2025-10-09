namespace HomeworkModule.Application.Interfaces;

public interface IHomeworkServiceClient
{
    Task<Result<bool>> CheckExistHomeworkById(Guid courseId, Guid id);
}
