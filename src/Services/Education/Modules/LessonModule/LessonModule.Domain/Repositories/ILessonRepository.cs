using LessonModule.Domain.Entities;
using SharedKernel.Domain.Repositories;

namespace LessonModule.Domain.Repositories;

public interface ILessonRepository : IRepository<LessonEntity>
{
}
