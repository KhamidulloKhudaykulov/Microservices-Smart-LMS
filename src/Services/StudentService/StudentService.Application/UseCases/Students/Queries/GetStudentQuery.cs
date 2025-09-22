using MediatR;
using StudentService.Application.UseCases.Students.Contracts;

namespace StudentService.Application.UseCases.Students.Queries;

public record GetStudentQuery() : IRequest<Result<IEnumerable<StudentResponseDto>>>;
    
public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, Result<IEnumerable<StudentResponseDto>>>
{
    
    public Task<Result<IEnumerable<StudentResponseDto>>> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}