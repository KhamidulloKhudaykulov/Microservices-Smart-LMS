using AccountService.Domain.Aggregates;
using SharedKernel.Domain.Specifications;

namespace AccountService.Domain.Interfaces;

public interface IAccountSpecification : ISpecification<AccountEntity>
{

}
