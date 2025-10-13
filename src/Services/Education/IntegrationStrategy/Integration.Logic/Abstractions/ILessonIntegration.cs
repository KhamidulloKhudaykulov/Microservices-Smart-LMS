using Integration.Logic.Dtos;

namespace Integration.Logic.Abstractions;

public interface ILessonIntegration
{
    Task<bool> ChechExistLessonByIdAsync(Guid lessonId);
    Task<LessonResponseDto?> GetLessonByIdAsync(Guid lessonId);
}
