using SharedKernel.Domain.Events;

namespace SharedKernel.Application.DomainEvents;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IReadOnlyCollection<IDomainEvent> domainEvents);
}
