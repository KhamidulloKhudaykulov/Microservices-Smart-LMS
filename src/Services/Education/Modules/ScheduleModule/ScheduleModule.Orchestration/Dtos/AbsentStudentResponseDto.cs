namespace ScheduleModule.Orchestration.Dtos;

public record AbsentStudentResponseDto(
    string Fullname,
    string Phonenumber,
    string Email,
    string Coursename,
    string LessonTheme,
    DateTime AbsetntsAt);
