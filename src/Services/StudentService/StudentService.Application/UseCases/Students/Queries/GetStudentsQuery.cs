using MediatR;
using StudentService.Domain.Entities;
using StudentService.Domain.Repositories;

namespace StudentService.Application.UseCases.Students.Queries;

public record GetStudentsQuery : IRequest<Result<IEnumerable<Student>>>;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, Result<IEnumerable<Student>>>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentsQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Result<IEnumerable<Student>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentRepository.SelectAllAsync();
        return Result.Success(students);
    }
}