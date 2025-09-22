using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Events;

namespace UserService.Domain.DomainEvents.Users;

public class UserCreatedDomainEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public UserCreatedDomainEvent(Guid userId, string email)
    {
        UserId = userId;
        Email = email;
    }
}
