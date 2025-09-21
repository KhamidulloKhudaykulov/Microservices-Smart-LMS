using UserService.Domain.Enums;
using UserService.Domain.Interfaces;
using UserService.Domain.States;

namespace UserService.Domain.Entities;

public class User : IEntity
{
    private User(Guid id, string userName)
    {
        Id = id;
        UserName = userName;
    }

    public Guid Id { get; set; }
    public string UserName { get; set; }
    public UserStatus UserStatus { get; internal set; }

    private IUserStatusState _userStatusState = new ActiveUserState();

    public static Result<User> Create(Guid id, string userName)
    {
        var result = new User(id, userName);
        result.ActivateUser();

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
