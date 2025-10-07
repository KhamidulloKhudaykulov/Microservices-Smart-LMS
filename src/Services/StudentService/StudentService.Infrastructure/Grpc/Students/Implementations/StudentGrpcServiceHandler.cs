using Grpc.Core;
using Newtonsoft.Json;
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

    public override async Task<GetStudentDetailsByIdResponse> GetStudentDetailsById(GetStudentDetailsByIdRequest request, ServerCallContext context)
    {
        var student = await _studentRepository
           .SelectAsync(u => u.Id == Guid.Parse(request.StudentId));

        if (student is null)
            return new GetStudentDetailsByIdResponse();

        var responseModel = new StudentGrpcModel
        {
            Id = student.Id.ToString(),
            Email = student.Email.Value,
            FullName = student.FullName.Value,
            Passportdata = student.PassportData.Value,
            Phonenumber = student.PhoneNumber.Value
        };

        var response = new GetStudentDetailsByIdResponse
        {
            Student = responseModel,
        };

        return response;
    }

    public override async Task<GetStudentAsJsonStringResponse> GetStudentAsJsonString(GetStudentAsJsonStringRequest request, ServerCallContext context)
    {
        var student = await _studentRepository
            .SelectAsync(u => u.Id == Guid.Parse(request.StudentId));

        if (student is null)
            return new GetStudentAsJsonStringResponse
            {
                Content = ""
            };

        var json = JsonConvert.SerializeObject(student);
        return new GetStudentAsJsonStringResponse
        {
            Content = json
        };
    }

    public override async Task<GetStudentsByIdsResponse> GetStudentsByIds(GetStudentsByIdsRequest request, ServerCallContext context)
    {
        var studentIds = request.StudentIds
           .Where(id => Guid.TryParse(id, out _))
           .Select(Guid.Parse)
           .ToList();

        if (!studentIds.Any())
            return new GetStudentsByIdsResponse();

        var students = await _studentRepository.GetAllByStudentIdsAsync(studentIds);

        if (!students.Any())
            return new GetStudentsByIdsResponse();

        var result = new GetStudentsByIdsResponse();

        foreach (var student in students)
        {
            result.Students.Add(new StudentGrpcModel
            {
                Id = student.Id.ToString(),
                FullName = student.FullName.Value,
                Email = student.Email.Value,
                Phonenumber = student.PhoneNumber.Value
            });
        }

        return result;
    }

}