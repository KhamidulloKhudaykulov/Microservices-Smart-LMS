using StudentService.Domain.Entities;

namespace StudentService.Domain.Interfaces;

public interface IStudentStatusState
{
    void ActivateStudent(Student student);
    void DeactivateStudent(Student student);
    void BlockStudent(Student student);
    void UnPaidTuition(Student student);
}
