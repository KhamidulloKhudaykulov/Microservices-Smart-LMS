using MediatR;
using StudentService.Application.Interfaces.Redis;
using StudentService.Application.UseCases.Students.Contracts;
using StudentService.Domain.Repositories;

namespace StudentService.Application.UseCases.Students.Queries;

public record GetStudentQuery() : IRequest<Result<IEnumerable<StudentResponseDto>>>;

public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, Result<IEnumerable<StudentResponseDto>>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IRedisCacheService _redisCacheService;

    public GetStudentQueryHandler(IStudentRepository studentRepository, IRedisCacheService redisCacheService)
    {
        _studentRepository = studentRepository;
        _redisCacheService = redisCacheService;
    }

    public Task<Result<IEnumerable<StudentResponseDto>>> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {


        throw new NotImplementedException();
    }
}