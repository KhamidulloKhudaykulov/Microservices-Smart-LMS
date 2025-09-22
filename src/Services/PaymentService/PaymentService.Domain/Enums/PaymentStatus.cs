namespace PaymentService.Domain.Enums;

public enum PaymentStatus
{
    Created,
    Processing,
    Completed,
    Failed,
    Cancelled,
    Refunded
}
