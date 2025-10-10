using Infrastructure.Grpc;
using StudentIntegration.Application.InterfaceBridges;
using StudentIntegration.Domain.Contracts;

namespace StudentIntegration.Infrastructure.Grpc.Client;

public class StudentGrpcServiceClient : IStudentServiceClient
{
    private readonly StudentGrpcService.StudentGrpcServiceClient _client;
    private readonly HttpClient _httpClient;

    private const string CacheBaseUrl = "https://localhost:7266/api/cache/students";

    public StudentGrpcServiceClient(StudentGrpcService.StudentGrpcServiceClient client, HttpClient httpClient)
    {
        _client = client;
        _httpClient = httpClient;
    }

    public async Task<List<StudentResponseContract>> GetStudentsByIds(List<Guid> studentIds)
    {
        var request = new GetStudentsByIdsRequest();
        request.StudentIds.AddRange(studentIds.Select(id => id.ToString()));

        var response = await _client.GetStudentsByIdsAsync(request);

        return response.Students.Select(s => new StudentResponseContract
        {
            Id = Guid.Parse(s.Id),
            FullName = s.FullName,
            Email = s.Email,
            PassportData = s.Passportdata,
            PhoneNumber = s.Phonenumber
        }).ToList();
    }

    public async Task<bool> VerifyExistStudentById(Guid studentId)
    {
        var url = $"{CacheBaseUrl}/verify/{studentId}";

        try
        {
            var response = await _httpClient.GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();
            var student = System.Text.Json.JsonSerializer.Deserialize<bool>(json);

            return student;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[VerifyExistStudentById] Error: {ex.Message}");
            return false;
        }
    }

    public async Task<StudentResponseContract> GetStudentDetailsById(Guid studentId)
    {
        var request = new GetStudentDetailsByIdRequest
        {
            StudentId = studentId.ToString()
        };

        var response = await _client.GetStudentDetailsByIdAsync(request);

        return new StudentResponseContract
        {
            Id = Guid.Parse(response.Student.Id),
            Email = response.Student.Email,
            FullName = response.Student.FullName,
            PassportData = response.Student.Passportdata,
            PhoneNumber = response.Student.Phonenumber
        };
    }
}
