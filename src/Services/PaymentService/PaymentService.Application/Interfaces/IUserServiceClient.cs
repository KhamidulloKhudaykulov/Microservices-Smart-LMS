namespace PaymentService.Application.Interfaces;

public interface IUserServiceClient
{
    Task<bool> IsUserAvailableAsync(Guid userId);
}
