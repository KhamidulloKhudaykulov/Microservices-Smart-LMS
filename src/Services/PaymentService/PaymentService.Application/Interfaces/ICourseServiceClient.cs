namespace PaymentService.Application.Interfaces;

public interface ICourseServiceClient
{
    Task<bool> IsCourseAvailableAsync(Guid courseId);
}
