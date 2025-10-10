using Infrastructure.Grpc;
using System.Text.Json;

namespace CacheGateway.Infrastructure.Grpc.Client;

public class StudentGrpcServiceClient
{
    private readonly StudentGrpcService.StudentGrpcServiceClient _client;

    public StudentGrpcServiceClient(StudentGrpcService.StudentGrpcServiceClient client)
    {
        _client = client;
    }

    public async Task<string?> GetStudentsByIds(List<Guid> studentIds)
    {
        var request = new GetStudentsByIdsRequest();
        request.StudentIds.AddRange(studentIds.Select(id => id.ToString()));

        var response = await _client.GetStudentsByIdsAsync(request);

        var json = JsonSerializer.Serialize(response.Students, new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return json;
    }

    public async Task<bool> VerifyExistStudentById(Guid studentId)
    {
        var request = new VerifyStudentRequest
        {
            StudentId = studentId.ToString()
        };

        var response = await _client.VerifyExistStudentAsync(request);

        return response.Exists;
    }

    public async Task<string?> GetStudentDetailsById(Guid studentId)
    {
        var request = new GetStudentDetailsByIdRequest
        {
            StudentId = studentId.ToString()
        };

        var response = await _client.GetStudentDetailsByIdAsync(request);

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return json;
    }
}