using StudentService.Domain.DomainEvents.Students;
using StudentService.Domain.Enums;
using StudentService.Domain.Interfaces;
using StudentService.Domain.Primitives;
using StudentService.Domain.States;
using StudentService.Domain.ValueObjects.Students;

namespace StudentService.Domain.Entities;

public class Student : Entity
{
    private Student(
        Guid id, 
        FullName fullname, 
        PhoneNumber phoneNumber, 
        PassportData passportData)
    {
        Id = id;
        FullName = fullname;
        PhoneNumber = phoneNumber;
        PassportData = passportData;
    }

    public FullName FullName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public PassportData PassportData { get; private set; }

    public StudentStatus StudentStatus { get; protected set; } = StudentStatus.Active;
    private IStudentStatusState _studentStatusState = new ActiveStudentState();

    public static Result<Student> Create(
        Guid id, 
        string fullName, 
        string phoneNumber, 
        string passportData)
    {
        var student = new Student(
            id, 
            FullName.Create(fullName).Value,
            PhoneNumber.Create(phoneNumber).Value,
            PassportData.Create(passportData).Value
            );

        student.AddDomainEvent(new StudentCreatedDomainEvent(student.Id, student.PhoneNumber));

        return student;
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