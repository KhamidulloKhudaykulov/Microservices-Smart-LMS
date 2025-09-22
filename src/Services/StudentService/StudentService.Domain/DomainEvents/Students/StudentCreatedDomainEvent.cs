using StudentService.Domain.Events;

namespace StudentService.Domain.DomainEvents.Students;

public class StudentCreatedDomainEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public StudentCreatedDomainEvent(Guid userId, string email)
    {
        UserId = userId;
        Email = email;
    }
}
