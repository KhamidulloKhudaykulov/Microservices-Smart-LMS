using Infrastructure.Grpc;
using StudentIntegration.Application.InterfaceBridges;
using StudentIntegration.Domain.Contracts;

namespace StudentIntegration.Infrastructure.Grpc.Client;

public class StudentGrpcServiceClient : IStudentServiceClient
{
    private readonly StudentGrpcService.StudentGrpcServiceClient _client;

    public StudentGrpcServiceClient(StudentGrpcService.StudentGrpcServiceClient client)
    {
        _client = client;
    }

    public async Task<List<StudentResponseContract>> GetStudentsByIds(List<Guid> studentIds)
    {
        var request = new GetStudentsByIdsRequest();
        request.StudentIds.AddRange(studentIds.Select(id => id.ToString()));

        var response = await _client.GetStudentsByIdsAsync(request);

        var result = response.Students.Select(s => new StudentResponseContract
        {
            Id = Guid.Parse(s.Id),
            FullName = s.FullName,
            Email = s.Email,
            PassportData = s.Passportdata,
            PhoneNumber = s.Phonenumber
        }).ToList();

        return result;
    }

    public async Task<StudentResponseContract> VerifyExistStudentById(Guid studentId)
    {
        var request = new VerifyStudentRequest
        {
            StudentId = studentId.ToString()
        };

        var response = await _client.VerifyExistStudentAsync(request);

        return new StudentResponseContract
        {
            Id = studentId,
        };
    }
}