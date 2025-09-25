using PaymentService.Domain.Enums;

namespace PaymentService.Application.UseCases.Payments.Contracts;

public class PaymentResponseDto
{
    public Guid AccountId { get; set; }
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public List<Month>? ForMonths { get; set; }
    public string? PaymentMethod { get; set; }
    public string? CancellationReason { get; set; }
}
