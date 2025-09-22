namespace StudentService.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
