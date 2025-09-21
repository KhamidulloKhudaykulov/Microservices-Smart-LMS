using StudentService.Domain.Entities;
using StudentService.Domain.Enums;
using StudentService.Domain.Interfaces;

namespace StudentService.Domain.States
{
    public class DeactiveStudentState : IStudentStatusState
    {
        public void ActivateStudent(Student student)
        {
            student.SetState(new ActiveStudentState());
            student.ChangeStatus(StudentStatus.Active);
        }

        public void BlockStudent(Student student)
        {
            student.SetState(new BlockStudentState());
            student.ChangeStatus(StudentStatus.Blocked);
        }

        public void DeactivateStudent(Student student)
        {
            Result.Failure(new Error(
                code: "Student.AlreadyIsDeactivated",
                message: "This student is already deactivated"));
        }

        public void UnPaidTuition(Student student)
        {
            student.SetState(new UnPaidStudentState());
        }
    }
}