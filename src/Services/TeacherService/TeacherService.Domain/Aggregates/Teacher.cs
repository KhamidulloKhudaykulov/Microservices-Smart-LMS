using SharedKernel.Domain.Primitives;
using TeacherService.Domain.Enums;
using TeacherService.Domain.ValueObjects.Teachers;

namespace TeacherService.Domain.Aggregates;

public class Teacher : AggregateRoot
{
    private Teacher(
        Guid id,
        TeacherName name,
        TeacherSurname surname,
        Email email,
        PhoneNumber phoneNumber) 
        : base(id)
    {
        Name = name;
        Surname = surname;
        Email = email;
        PhoneNumber = phoneNumber;
        Status = TeacherStatus.Active;
    }

    public TeacherName Name { get; private set; }
    public TeacherSurname Surname { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public TeacherStatus Status { get; private set; }

    public static Result<Teacher> Create(
        Guid id,
        TeacherName name,
        TeacherSurname surname,
        Email email,
        PhoneNumber phoneNumber)
    {
        return Result.Success(new Teacher(
            id, name, surname, email, phoneNumber));
    }

    public Result Update(
    string? name,
    string? surname,
    string? email,
    string? phoneNumber)
    {
        if (!string.IsNullOrEmpty(name)
        && name != Name.Value)
        {
            var nameResult = TeacherName.Create(name);
            if (nameResult.IsFailure)
                return Result.Failure(nameResult.Error);

            Name = nameResult.Value;
        }

        if (!string.IsNullOrEmpty(surname)
            && surname != Surname.Value)
        {
            var surnameResult = TeacherSurname.Create(surname);
            if (surnameResult.IsFailure)
                return Result.Failure(surnameResult.Error);

            Surname = surnameResult.Value;
        }

        if (!string.IsNullOrEmpty(email)
            && email != Email.Value)
        {
            var emailResult = Email.Create(email);
            if (emailResult.IsFailure)
                return Result.Failure(emailResult.Error);

            Email = emailResult.Value;
        }

        if (!string.IsNullOrEmpty(phoneNumber)
            && phoneNumber != PhoneNumber.Value)
        {
            var phoneResult = PhoneNumber.Create(phoneNumber);
            if (phoneResult.IsFailure)
                return Result.Failure(phoneResult.Error);

            PhoneNumber = phoneResult.Value;
        }

        return Result.Success();
    }


    public void Activate() => Status = TeacherStatus.Active;
    public void Deactivate() => Status = TeacherStatus.Inactive;
}
