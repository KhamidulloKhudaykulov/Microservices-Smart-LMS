using StudentIntegration.Domain.Contracts;

namespace StudentIntegration.Application.InterfaceBridges;

public interface IStudentServiceClient
{
    //Task<bool> CheckExistStudentByIdAsync(Guid studentId);
    Task<bool> VerifyExistStudentById(Guid studentId);
    Task<List<StudentResponseContract>> GetStudentsByIds(List<Guid> studentIds);
    Task<StudentResponseContract> GetStudentDetailsById(Guid studentId);
}
