using StudentIntegration.Domain.Contracts;

namespace StudentIntegration.Application.InterfaceBridges;

public interface IStudentServiceClient
{
    Task<StudentResponseContract> VerifyExistStudentById(Guid studentId);
    Task<List<StudentResponseContract>> GetStudentsByIds(List<Guid> studentIds);
}
