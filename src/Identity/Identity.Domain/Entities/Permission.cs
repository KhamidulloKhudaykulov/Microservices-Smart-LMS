using Identity.Domain.Shared;

namespace Identity.Domain.Entities;

public class Permission
{
    private Permission(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public static Result<Permission> Create(Guid id, string name)
    {
        return new Permission(id, name);
    }
}
