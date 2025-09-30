namespace CourseModule.Domain.Entitites;

public class StudentPayment
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }

    public List<Guid>? PaymentIds { get; set; }
}
