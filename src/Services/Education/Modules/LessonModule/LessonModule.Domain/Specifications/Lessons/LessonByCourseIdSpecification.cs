using LessonModule.Domain.Entities;

namespace LessonModule.Domain.Specifications.Lessons;

public class LessonByCourseIdSpecification : BaseSpecification<LessonEntity>
{
    public LessonByCourseIdSpecification(Guid CourseId)
    {
        Criteria = lesson => lesson.CourseId == CourseId;

        ApplyOrderByDescending(lesson => lesson.Date);
    }
}
