using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;
/// <summary>
/// Here used State design pattern and this is a base interface for all classes which responsible state of entity
/// </summary>
public interface IUserStatusState
{
    void ActivateUser(User user);
    void DeactivateUser(User user);
    void BlockUser(User user);
}
