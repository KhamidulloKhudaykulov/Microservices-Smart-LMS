using UserService.Domain.Entities;
using UserService.Domain.Enums;
using UserService.Domain.Interfaces;

namespace UserService.Domain.States;

public class DeactiveUserState : IUserStatusState
{
    public void ActivateUser(User user)
    {
        user.SetState(new ActiveUserState());
        user.ChangeStatus(UserStatus.Active);
    }

    public void BlockUser(User user)
    {
        user.SetState(new BlockUserState());
        user.ChangeStatus(UserStatus.Blocked);
    }

    public void DeactivateUser(User user)
    {
        Result.Failure(new Error(
             code: "User.AlreadyIsDeactivated",
             message: "This user is already deactivated"));
    }
}
