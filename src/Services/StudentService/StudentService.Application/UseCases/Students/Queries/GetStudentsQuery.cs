using MediatR;
using StudentService.Application.Helpers;
using StudentService.Application.Interfaces.Redis;
using StudentService.Application.UseCases.Students.Contracts;
using StudentService.Domain.Entities;
using StudentService.Domain.Repositories;

namespace StudentService.Application.UseCases.Students.Queries;

public record GetStudentsQuery(
    int PageNumber,
    int PageSize) : IRequest<Result<IEnumerable<StudentResponseDto>>>;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, Result<IEnumerable<StudentResponseDto>>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IRedisCacheService _redisCacheService;

    public GetStudentsQueryHandler(IStudentRepository studentRepository, IRedisCacheService redisCacheService)
    {
        _studentRepository = studentRepository;
        _redisCacheService = redisCacheService;
    }

    public async Task<Result<IEnumerable<StudentResponseDto>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = RedisHelper.GenerateKey("students", new
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        });
        
        var items = await _redisCacheService
            .GetAsync<IEnumerable<StudentResponseDto>>(cacheKey);
        
        if (items is not null)
            return Result.Success<IEnumerable<StudentResponseDto>>(items);
        
        var students = (await _studentRepository.SelectAllAsync())
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(s => new StudentResponseDto()
            {
                Id = s.Id,
                FullName = s.FullName,
                PhoneNumber = s.PhoneNumber,
                PassportData = s.PassportData,
            });

        await _redisCacheService.SetAsync(cacheKey, students);
        await _redisCacheService.SetExpireAsync(cacheKey, TimeSpan.FromMinutes(60));
        
        return Result.Success(students);
    }
}