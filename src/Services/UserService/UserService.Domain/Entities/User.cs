using UserService.Domain.DomainEvents.Users;
using UserService.Domain.Enums;
using UserService.Domain.Interfaces;
using UserService.Domain.Primitives;
using UserService.Domain.States;

namespace UserService.Domain.Entities;

public class User : Entity
{
    private User(Guid id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }

    public string UserName { get; set; }
    public string Email { get; set; }
    public UserStatus UserStatus { get; internal set; }

    private IUserStatusState _userStatusState = new ActiveUserState();

    public static Result<User> Create(Guid id, string userName, string email)
    {
        var result = new User(id, userName, email);

        result.AddDomainEvent(new UserCreatedDomainEvent(result.Id, result.Email));

        return result;
    }

    internal void ChangeStatus(UserStatus newStatus)
    {
        UserStatus = newStatus;
    }

    public void SetState(IUserStatusState state)
    {
        _userStatusState = state;
    }

    public void ActivateUser() => _userStatusState.ActivateUser(this);
    public void DeactivateUser() => _userStatusState.DeactivateUser(this);
    public void BlockUser() => _userStatusState.BlockUser(this);

}
