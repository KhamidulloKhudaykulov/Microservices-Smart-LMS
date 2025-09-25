using AccountService.Domain.Entities;
using AccountService.Domain.Interfaces;
using System.Linq.Expressions;

namespace AccountService.Domain.Specifications;

internal class BaseAccountSpecification : IAccountSpecification
{
    public Expression<Func<AccountEntity, bool>>? Criteria { get; set; }

    public List<Expression<Func<AccountEntity, object>>> Includes { get; } = new();

    public Expression<Func<AccountEntity, object>>? OrderBy { get; protected set; }

    public Expression<Func<AccountEntity, object>>? OrderByDescending { get; protected set; }

    public bool? IsPagingEnabled => Skip.HasValue && Take.HasValue;
    public int? Skip { get; protected set; }
    public int? Take { get; protected set; }

    protected void AddInclude(Expression<Func<AccountEntity, object>> includeExpression)
        => Includes.Add(includeExpression);

    protected void ApplyOrderBy(Expression<Func<AccountEntity, object>> orderByExpression)
     => OrderBy = orderByExpression;

    protected void ApplyOrderByDescending(Expression<Func<AccountEntity, object>> orderByDescExpression)
        => OrderByDescending = orderByDescExpression;

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }
}
