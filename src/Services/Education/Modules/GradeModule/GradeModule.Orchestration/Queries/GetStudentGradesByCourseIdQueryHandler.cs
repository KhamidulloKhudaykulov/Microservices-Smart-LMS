﻿using CourseModule.Application.Interfaces;
using GradeModule.Domain.Repositories;
using GradeModule.Orchestration.Dtos;
using Integration.Logic.Abstractions;
using SharedKernel.Application.Abstractions.Messaging;
using StudentIntegration.Application.InterfaceBridges;

namespace GradeModule.Orchestration.Queries;

public record GetStudentGradesByCourseIdQuery(
    Guid studentId,
    Guid courseId)
    : IQuery<List<StudentGradeDto>>;

public class GetStudentGradesByCourseIdQueryHandler(
    IGradeRepository _gradeRepository,
    IStudentServiceClient _studentServiceClient,
    ICourseIntegration _courseIntegration)
    : IQueryHandler<GetStudentGradesByCourseIdQuery, List<StudentGradeDto>>
{
    public async Task<Result<List<StudentGradeDto>>> Handle(GetStudentGradesByCourseIdQuery request, CancellationToken cancellationToken)
    {
        var studentGrades = _gradeRepository.SelectAsQueryable(
            g => g.StudentId == request.studentId
            && g.CourseId == request.courseId).ToList();

        if (!studentGrades.Any())
            return new List<StudentGradeDto>();

        var studentTask = _studentServiceClient.GetStudentDetailsById(request.studentId);
        var courseTask = _courseIntegration.GetCourseByIdAsync(request.courseId);

        await Task.WhenAll(studentTask, courseTask);

        var student = studentTask.Result;
        var course = courseTask.Result;

        var result = studentGrades.Select(
            sg => new StudentGradeDto(student.FullName, sg.AssignedAt, sg.Score, sg.Feedback));

        return result.ToList();
    }
}
