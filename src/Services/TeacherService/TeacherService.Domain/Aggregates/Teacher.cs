using SharedKernel.Domain.Primitives;

namespace TeacherService.Domain.Aggregates;

public class Teacher : AggregateRoot
{
    private Teacher(
        Guid id,
        string name,
        string surname,
        string email,
        string phoneNumber) 
        : base(id)
    {
        Name = name;
        Surname = surname;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }

    public static Result<Teacher> Create(
        Guid id,
        string name,
        string surname,
        string email,
        string phoneNumber)
    {
        return Result.Success(new Teacher(
            id, name, surname, email, phoneNumber));
    }
}
