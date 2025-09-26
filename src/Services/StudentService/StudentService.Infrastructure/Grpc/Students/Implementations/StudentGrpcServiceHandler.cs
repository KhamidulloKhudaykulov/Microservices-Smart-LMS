using Grpc.Core;
using StudentService.Domain.Repositories;

namespace StudentService.Infrastructure.Grpc.Students.Implementations;

public class StudentGrpcServiceHandler(
    IStudentRepository _studentRepository) : StudentGrpcService.StudentGrpcServiceBase
{
    public override async Task<VerifyStudentResponse> VerifyExistStudent(
        VerifyStudentRequest request,
        ServerCallContext context)
    {
        var student = await _studentRepository
            .SelectAsync(u => u.Id == Guid.Parse(request.StudentId));

        return new VerifyStudentResponse
        {
            Exists = student is not null
        };
    }
}