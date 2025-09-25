using SharedKernel.Domain.Primitives;

namespace AccountService.Domain.Entities;

public class AccountEntity : Entity
{
    public string AccountName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Status { get; private set; }
}
