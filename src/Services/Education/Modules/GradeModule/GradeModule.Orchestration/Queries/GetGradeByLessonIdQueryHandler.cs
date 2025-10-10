using GradeModule.Domain.Repositories;
using GradeModule.Orchestration.Dtos;
using SharedKernel.Application.Abstractions.Messaging;
using StudentIntegration.Application.InterfaceBridges;

namespace GradeModule.Orchestration.Queries;

public record GetGradesByLessonIdQuery(
    Guid LessonId,
    Guid StudentId)
    : IQuery<StudentGradeDto>;

public class GetGradesByLessonIdQueryHandler(
    IGradeRepository _gradeRepository,
    IStudentServiceClient _studentServiceClient)
    : IQueryHandler<GetGradesByLessonIdQuery, StudentGradeDto>
{
    public async Task<Result<StudentGradeDto>> Handle(GetGradesByLessonIdQuery request, CancellationToken cancellationToken)
    {
        var grade = await _gradeRepository.SelectAsync(
            g => g.StudentId == request.StudentId
            && g.LessonId == request.LessonId);

        if (grade is null)
            return Result.Failure<StudentGradeDto>(new Error(
                code: "Grade.NotFound",
                message: "This student was not graded at this date"));

        var student = await _studentServiceClient.GetStudentDetailsById(request.StudentId);

        var result = new StudentGradeDto(student.FullName, grade.AssignedAt, grade.Score, grade.Feedback);

        return Result.Success(result);
    }
}