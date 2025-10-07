using LessonModule.Application.Contracts;

namespace LessonModule.Application.Interfaces;

public interface ILessonServiceClient
{
    Task<bool> ChechExistLessonByIdAsync(Guid lessonId);
    Task<LessonResponseDto?> GetLessonByIdAsync(Guid lessonId);
}
