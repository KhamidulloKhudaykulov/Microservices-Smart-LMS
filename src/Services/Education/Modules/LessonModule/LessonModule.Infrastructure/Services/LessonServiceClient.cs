using LessonModule.Application.Contracts;
using LessonModule.Application.Interfaces;
using LessonModule.Domain.Repositories;

namespace LessonModule.Infrastructure.Services;

public class LessonServiceClient(
    ILessonRepository _lessonRepository) 
    : ILessonServiceClient
{
    public async Task<bool> ChechExistLessonByIdAsync(Guid lessonId)
    {
        var existLesson = await _lessonRepository
            .SelectByIdAsync(lessonId);

        return existLesson is not null;
    }

    public async Task<LessonResponseDto?> GetLessonByIdAsync(Guid lessonId)
    {
        var existLesson = await _lessonRepository
            .SelectByIdAsync(lessonId);

        if (existLesson is null)
            return null;

        return new LessonResponseDto(existLesson.CourseId, existLesson.Theme.Value, existLesson.Date);
    }
}
