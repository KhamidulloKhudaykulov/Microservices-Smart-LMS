using SharedKernel.Application.Abstractions.Messaging;
using TeacherService.Application.UseCases.Teachers.Contracts;
using TeacherService.Domain.Repositories;

namespace TeacherService.Application.UseCases.Teachers.Queries;

public record GetTeacherByIdQuery(Guid Id) : IQuery<TeacherResponseDto>;

public class GetTeacherByIdQueryHandler(
    ITecherRepository _teacherRepository)
    : IQueryHandler<GetTeacherByIdQuery, TeacherResponseDto>
{
    public async Task<Result<TeacherResponseDto>> Handle(
        GetTeacherByIdQuery request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.SelectByIdAsync(request.Id);
        if (teacher is null)
            return Result.Failure<TeacherResponseDto>(new Error(
                code: "Teacher.NotFound",
                message: $"Teacher with ID={request.Id} was not found"));

        var response = new TeacherResponseDto(
            teacher.Id,
            teacher.Name.Value,
            teacher.Surname.Value,
            teacher.Email.Value,
            teacher.PhoneNumber.Value,
            teacher.Status.ToString()
        );

        return Result.Success(response);
    }
}
