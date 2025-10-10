using MediatR;
using ScheduleModule.Domain.Entities;
using ScheduleModule.Domain.Repositories;
using SharedKernel.Application.Abstractions.Messaging;

namespace ScheduleModule.Application.UseCases.LessonSchedules.Commands;

public record MarkStudentsPresentAtLessonCommand(
    Guid LessonId,
    List<Guid> StudentIds) : ICommand<Unit>;

public class MarkStudentsPresentAtLessonCommandHandler(
    IScheduleRepository<LessonScheduleEntity> _scheduleRepository,
    IScheduleUnitOfWork _unitOfWork)
    : ICommandHandler<MarkStudentsPresentAtLessonCommand, Unit>
{
    public async Task<Result<Unit>> Handle(MarkStudentsPresentAtLessonCommand request, CancellationToken cancellationToken)
    {
        var existSchedule = await _scheduleRepository
            .SelectAsync(e => e.LessonId == request.LessonId);

        if (existSchedule is not null)
        {
            existSchedule.MarkStudentsPresent(request.StudentIds);

            await _scheduleRepository.UpdateAsync(existSchedule);
        }
        else
        {
            var entity = LessonScheduleEntity.Create(request.LessonId).Value;
            entity.MarkStudentsPresent(request.StudentIds);

            await _scheduleRepository.InsertAsync(entity);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(Unit.Value);
    }
}
