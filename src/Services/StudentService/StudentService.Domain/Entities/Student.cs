using StudentService.Domain.Enums;
using StudentService.Domain.Interfaces;
using StudentService.Domain.States;

namespace StudentService.Domain.Entities;

public class Student : IEntity
{
    private Student(
        Guid id, 
        string fullname, 
        string phoneNumber, 
        string passportData)
    {
        Id = id;
        FullName = fullname;
        PhoneNumber = phoneNumber;
        PassportData = passportData;
    }

    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string PassportData { get; private set; }

    public StudentStatus StudentStatus { get; protected set; } = StudentStatus.Active;
    private IStudentStatusState _studentStatusState = new ActiveStudentState();

    public static Result<Student> Create(
        Guid id, 
        string fullName, 
        string phoneNumber, 
        string passportData)
    {
        var result = new Student(id, fullName, phoneNumber, passportData);
        return result;
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