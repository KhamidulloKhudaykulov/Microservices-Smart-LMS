using CourseModule.Domain.Exceptions;
using CourseModule.Domain.Repositories;
using CourseModule.Orchestration.Dtos;
using SharedKernel.Application.Abstractions.Messaging;
using StudentIntegration.Application.InterfaceBridges;

namespace CourseModule.Orchestration.Queries;

public record GetCourseWithStudentsQuery(
    Guid CourseId) : IQuery<IEnumerable<StudentDto>>;

public class GetCourseWithStudentsQueryHandler(
    IStudentServiceClient _studentServiceClient,
    ICourseRepository _courseRepository) 
    : IQueryHandler<GetCourseWithStudentsQuery, IEnumerable<StudentDto>>
{
    public async Task<Result<IEnumerable<StudentDto>>> Handle(GetCourseWithStudentsQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository
            .SelectByIdAsync(request.CourseId);

        if (course is null)
            return Results
                .NotFoundException<IEnumerable<StudentDto>>(
                CourseErrors.NotFound);

        var students = (await _studentServiceClient
            .GetStudentsByIds(course.StudentIds))
            .Select(s => new StudentDto(
                Id: s.Id,
                Fullname: s.FullName,
                Email: s.Email,
                Phonenumber: s.PhoneNumber,
                PassportData: s.PassportData));

        return Result.Success(students);
    }
}
