namespace StudentService.Domain.Interfaces;
/// <summary>
/// Base interface for all entities which must has an integration with database
/// </summary>
public interface IEntity
{
    Guid Id { get; }
}
