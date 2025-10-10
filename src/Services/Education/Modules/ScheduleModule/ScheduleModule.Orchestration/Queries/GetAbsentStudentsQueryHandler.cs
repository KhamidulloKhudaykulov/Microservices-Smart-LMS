using CourseModule.Application.Interfaces;
using LessonModule.Application.Interfaces;
using ScheduleModule.Domain.Entities;
using ScheduleModule.Domain.Repositories;
using ScheduleModule.Orchestration.Dtos;
using SharedKernel.Application.Abstractions.Messaging;
using StudentIntegration.Application.InterfaceBridges;

namespace ScheduleModule.Orchestration.Queries;

public record GetAbsentStudentsQuery(
    Guid LessonId) : IQuery<List<AbsentStudentResponseDto>>;

public class GetAbsentStudentsQueryHandler(
    IScheduleRepository<LessonScheduleEntity> _scheduleRepository,
    IStudentServiceClient _studentServiceClient,
    ICourseServiceClient _courseServiceClient,
    ILessonServiceClient _lessonServiceClient) : IQueryHandler<GetAbsentStudentsQuery, List<AbsentStudentResponseDto>>
{
    public async Task<Result<List<AbsentStudentResponseDto>>> Handle(GetAbsentStudentsQuery request, CancellationToken cancellationToken)
    {
        var details = await _scheduleRepository
            .SelectAsync(s => s.LessonId == request.LessonId);

        if (details is null)
            return Result.Failure<List<AbsentStudentResponseDto>>(new Error(
                code: "Lesson.NotFound",
                message: "Lesson was not found"));

        var absentStudents = details.AbsentStudents;

        if (absentStudents is null)
            return Result.Failure<List<AbsentStudentResponseDto>>(new Error(
                code: "AbsetStudents.NotFound",
                message: "Absent students was not found in this Lesson"));

        var lesson = await _lessonServiceClient.GetLessonByIdAsync(request.LessonId);

        var studentTask = _studentServiceClient.GetStudentsByIds(absentStudents);
        var courseTask = _courseServiceClient.GetCourseByIdAsync(lesson!.CourseId);

        await Task.WhenAll(studentTask, courseTask);

        var students = await studentTask;
        var course = await courseTask;

        var result = students.Select(s => new AbsentStudentResponseDto(
            Fullname: s.FullName,
            Phonenumber: s.PhoneNumber,
            LessonTheme: lesson.Theme,
            Email: s.Email,
            Coursename: course.Value!.Name,
            AbsetntsAt: details.CreatedAt))
            .ToList();

        return Result.Success(result);
    }
}
