using StudentService.Domain.Events;

namespace StudentService.Application.Interfaces.DomainEvents;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IReadOnlyCollection<IDomainEvent> domainEvents);
}
