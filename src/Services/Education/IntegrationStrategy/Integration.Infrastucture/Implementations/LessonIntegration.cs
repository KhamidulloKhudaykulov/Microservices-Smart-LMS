using Integration.Logic.Abstractions;
using Integration.Logic.Dtos;
using LessonModule.Domain.Repositories;

namespace Integration.Infrastucture.Implementations;

public class LessonIntegration : ILessonIntegration
{
    private readonly ILessonRepository _lessonRepository;

    public LessonIntegration(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<bool> ChechExistLessonByIdAsync(Guid lessonId)
    {
        var existLesson = await _lessonRepository.SelectByIdAsync(lessonId);
        return existLesson is not null;
    }

    public async Task<LessonResponseDto?> GetLessonByIdAsync(Guid lessonId)
    {
        var existLesson = await _lessonRepository.SelectByIdAsync(lessonId);

        if (existLesson is null)
            return null;

        return new LessonResponseDto(
            existLesson.CourseId,
            existLesson.Theme.Value,
            existLesson.Date
        );
    }
}
