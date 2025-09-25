using SharedKernel.Application.DomainEvents;
using SharedKernel.Domain.Events;

namespace SharedKernel.Infrastructure.DomainEvents;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    public Task DispatchAsync(IReadOnlyCollection<IDomainEvent> domainEvents)
    {
        throw new NotImplementedException();
    }
}
