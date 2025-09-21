using UserService.Domain.Entities;
using UserService.Domain.Enums;
using UserService.Domain.Interfaces;

namespace UserService.Domain.States;

public class ActiveUserState : IUserStatusState
{
    public void ActivateUser(User user)
    {
        user.SetState(new ActiveUserState());
        user.UserStatus = UserStatus.Active;
    }

    public void BlockUser(User user)
    {
        user.SetState(new BlockUserState());
        user.ChangeStatus(UserStatus.Blocked);
    }

    public void DeactivateUser(User user)
    {
        user.SetState(new DeactiveUserState());
        user.ChangeStatus(UserStatus.Inactive);
    }
}
