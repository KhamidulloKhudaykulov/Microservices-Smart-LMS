using AccountService.Domain.Entities;
using SharedKernel.Domain.Specifications;

namespace AccountService.Domain.Interfaces;

public interface IAccountSpecification : ISpecification<AccountEntity>
{

}
