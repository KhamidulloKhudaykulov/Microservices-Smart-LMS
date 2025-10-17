using StudentService.Domain.DomainEvents.Students;
using StudentService.Domain.Enums;
using StudentService.Domain.Interfaces;
using StudentService.Domain.Primitives;
using StudentService.Domain.States;
using StudentService.Domain.ValueObjects.Students;

namespace StudentService.Domain.Entities;

public class Student : Entity
{
    protected Student() { }
    private Student(
        Guid id,
        FullName fullname,
        PhoneNumber phoneNumber,
        PassportData passportData,
        Email email,
        UniqueCode uniqueCode)
    {
        Id = id;
        FullName = fullname;
        PhoneNumber = phoneNumber;
        PassportData = passportData;
        Email = email;
        UniqueCode = uniqueCode;
    }

    public UniqueCode UniqueCode { get; private set; }
    public FullName FullName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public PassportData PassportData { get; private set; }
    public Email Email { get; private set; }

    public StudentStatus StudentStatus { get; protected set; } = StudentStatus.Active;
    private IStudentStatusState _studentStatusState = new ActiveStudentState();

    public static Result<Student> Create(
        Guid id,
        string fullName,
        string phoneNumber,
        string passportData,
        string email)
    {
        var fullNameResult = FullName.Create(fullName);
        if (fullNameResult.IsFailure)
            return Result.Failure<Student>(fullNameResult.Error);

        var phoneNumberResult = PhoneNumber.Create(phoneNumber);
        if (phoneNumberResult.IsFailure)
            return Result.Failure<Student>(phoneNumberResult.Error);

        var passportDataResult = PassportData.Create(passportData);
        if (passportDataResult.IsFailure)
            return Result.Failure<Student>(passportDataResult.Error);

        var emailResult = Email.Create(email);
        if (emailResult.IsFailure)
            return Result.Failure<Student>(emailResult.Error);

        var uniqueCodeResult = UniqueCode.Create();
        if (uniqueCodeResult.IsFailure)
            return Result.Failure<Student>(uniqueCodeResult.Error);

        var student = new Student(
            id,
            fullNameResult.Value,
            phoneNumberResult.Value,
            passportDataResult.Value,
            emailResult.Value,
            uniqueCodeResult.Value
        );

        student.AddDomainEvent(new StudentCreatedDomainEvent(student.Id, student.Email.Value));

        return Result.Success(student);
    }


    public Result<Student> Update(
        string fullName,
        string phoneNumber,
        string passportData)
    {
        var fullNameResult = FullName.Create(fullName);
        var phoneNumberResult = PhoneNumber.Create(phoneNumber);
        var passportDataResult = PassportData.Create(passportData);

        if (fullNameResult.IsFailure)
            return Result.Failure<Student>(fullNameResult.Error);
        if (phoneNumberResult.IsFailure)
            return Result.Failure<Student>(phoneNumberResult.Error);
        if (passportDataResult.IsFailure)
            return Result.Failure<Student>(passportDataResult.Error);

        FullName = fullNameResult.Value;
        PhoneNumber = phoneNumberResult.Value;
        PassportData = passportDataResult.Value;

        return Result.Success(this);
    }

    internal void ChangeStatus(StudentStatus newStatus)
    {
        StudentStatus = newStatus;
    }

    public void SetState(IStudentStatusState state)
    {
        _studentStatusState = state;
    }

    public void ActivateStudent() => _studentStatusState.ActivateStudent(this);
    public void DeactivateStudent() => _studentStatusState.DeactivateStudent(this);
    public void BlockStudent() => _studentStatusState.BlockStudent(this);
    public void UnPaidTuition() => _studentStatusState.UnPaidTuition(this);
}