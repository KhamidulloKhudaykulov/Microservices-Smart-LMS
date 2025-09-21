using UserService.Domain.Entities;
using UserService.Domain.Enums;
using UserService.Domain.Interfaces;

namespace UserService.Domain.States;

public class BlockUserState : IUserStatusState
{
    public void ActivateUser(User user)
    {
        user.SetState(new ActiveUserState());
        user.ChangeStatus(UserStatus.Active);
    }

    public void BlockUser(User user)
    {
        Result.Failure(new Error(
            code: "User.AlreadyIsBlocked",
            message: "This user is already blocked"));
    }

    public void DeactivateUser(User user)
    {
        user.SetState(new DeactiveUserState());
        user.ChangeStatus(UserStatus.Inactive);
    }
}
