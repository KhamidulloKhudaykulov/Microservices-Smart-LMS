using StudentService.Application.Interfaces.DomainEvents;
using StudentService.Domain.Events;

namespace StudentService.Infrastructure.DomainEvents;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    public Task DispatchAsync(IReadOnlyCollection<IDomainEvent> domainEvents)
    {
        throw new NotImplementedException();
    }
}
