using Identity.Domain.Shared;

namespace Identity.Domain.Entities;

public class Role
{
    private Role(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public static Result<Role> Create(Guid id, string name)
    {
        return new Role(id, name);
    }

    public void AddPermission(Permission permission)
    {
        RolePermissions.Add(new RolePermission
        {
            Permission = permission,
            PermissionId = permission.Id,
            Role = this,
            RoleId = this.Id
        });
    }
}
