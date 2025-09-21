using StudentService.Domain.Entities;
using StudentService.Domain.Enums;
using StudentService.Domain.Interfaces;

namespace StudentService.Domain.States
{
    public class BlockStudentState : IStudentStatusState
    {
        public void ActivateStudent(Student student)
        {
            student.SetState(new ActiveStudentState());
            student.ChangeStatus(StudentStatus.Active);
        }

        public void BlockStudent(Student student)
        {
            Result.Failure(new Error(
                code: "Student.AlreadyIsBlocked",
                message: "This student is already blocked"));
        }

        public void DeactivateStudent(Student student)
        {
            student.SetState(new DeactiveStudentState());
            student.ChangeStatus(StudentStatus.Inactive);
        }

        public void UnPaidTuition(Student student)
        {
            student.SetState(new UnPaidStudentState());
            student.ChangeStatus(StudentStatus.Unpaid);
        }
    }
}