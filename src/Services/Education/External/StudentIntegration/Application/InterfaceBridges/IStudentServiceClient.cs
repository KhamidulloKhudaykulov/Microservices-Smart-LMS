using Domain.Contracts;

namespace Application.InterfaceBridges;

public interface IStudentServiceClient
{
    Task<StudentResponseContract> VerifyExistStudentById(Guid studentId);
}
