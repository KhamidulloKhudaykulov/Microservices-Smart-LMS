using SharedKernel.Application.Abstractions.Messaging;
using TeacherService.Application.UseCases.Teachers.Contracts;
using TeacherService.Domain.Repositories;

namespace TeacherService.Application.UseCases.Teachers.Queries;

public record GetAllTeachersQuery(int Page = 1, int PageSize = 10)
    : IQuery<PagedList<TeacherResponseDto>>;

public class GetAllTeachersQueryHandler(
    ITecherRepository _teacherRepository)
    : IQueryHandler<GetAllTeachersQuery, PagedList<TeacherResponseDto>>
{
    public async Task<Result<PagedList<TeacherResponseDto>>> Handle(
        GetAllTeachersQuery request,
        CancellationToken cancellationToken)
    {
        var teachers = await _teacherRepository.SelectAllAsync();
        if (teachers is null || !teachers.Any())
            return Result.Failure<PagedList<TeacherResponseDto>>(new Error(
                code: "Teacher.NotFound",
                message: "No teachers found."));

        var totalCount = teachers.Count();
        var skip = (request.Page - 1) * request.PageSize;

        var teacherDtos = teachers
            .Skip(skip)
            .Take(request.PageSize)
            .Select(t => new TeacherResponseDto(
                t.Id,
                t.Name.Value,
                t.Surname.Value,
                t.Email.Value,
                t.PhoneNumber.Value,
                t.Status.ToString()))
            .ToList();

        var pagedItems = teacherDtos
            .ToList();

        var pagedList = new PagedList<TeacherResponseDto>(
            pagedItems,
            totalCount,
            request.Page,
            request.PageSize);

        return Result.Success(pagedList);
    }
}
