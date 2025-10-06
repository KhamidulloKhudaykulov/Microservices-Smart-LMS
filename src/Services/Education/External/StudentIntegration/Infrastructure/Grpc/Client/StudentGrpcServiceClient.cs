using Application.InterfaceBridges;
using Domain.Contracts;
using Grpc.Core;

namespace Infrastructure.Grpc.Client;

public class StudentGrpcServiceClient : IStudentServiceClient
{
    private readonly StudentGrpcService.StudentGrpcServiceClient _client;

    public StudentGrpcServiceClient(StudentGrpcService.StudentGrpcServiceClient client)
    {
        _client = client;
    }

    public VerifyStudentResponse VerifyExistStudent(VerifyStudentRequest request)
    {
        Console.WriteLine($"Verifying student: {request.StudentId}");

        var response = _client.VerifyExistStudent(request);

        Console.WriteLine($"Response: {response.Exists}");

        return response;
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