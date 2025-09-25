namespace SharedKernel.Domain.Events;

public class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
