using Identity.Domain.Shared;

namespace Identity.Domain.Entities;

public class User
{
    private User(Guid id, string userName, string password)
    {
        Id = id;
        UserName = userName;
        Password = password;
        UserId = id;
    }
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public static Result<User> Create(Guid id, string userName, string password)
    {
        return new User(id, userName, password);
    }

    public void AddRole(Role role)
    {
        this.UserRoles.Add(new UserRole
        {
            Id = Guid.NewGuid(),
            RoleId = role.Id,
            Role = role,
            User = this,
            UserId = this.Id
        });
    }
}
