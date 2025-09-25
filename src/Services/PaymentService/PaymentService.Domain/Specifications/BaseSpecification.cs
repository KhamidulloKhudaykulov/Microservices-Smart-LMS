using PaymentService.Domain.Entities;
using PaymentService.Domain.Interfaces;
using System.Linq.Expressions;

namespace PaymentService.Domain.Specifications;

public abstract class BaseSpecification : IPaymentSpecification
{
    public Expression<Func<PaymentEntity, bool>>? Criteria { get; protected set; }
    public List<Expression<Func<PaymentEntity, object>>> Includes { get; } = new();
    public Expression<Func<PaymentEntity, object>>? OrderBy { get; protected set; }
    public Expression<Func<PaymentEntity, object>>? OrderByDescending { get; protected set; }

    public bool? IsPagingEnabled => Skip.HasValue && Take.HasValue;
    public int? Skip { get; protected set; }
    public int? Take { get; protected set; }
    public Guid AccountId { get; protected set; }

    protected void AddInclude(Expression<Func<PaymentEntity, object>> includeExpression)
        => Includes.Add(includeExpression);

    protected void ApplyOrderBy(Expression<Func<PaymentEntity, object>> orderByExpression)
     => OrderBy = orderByExpression;

    protected void ApplyOrderByDescending(Expression<Func<PaymentEntity, object>> orderByDescExpression)
        => OrderByDescending = orderByDescExpression;

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }
}
