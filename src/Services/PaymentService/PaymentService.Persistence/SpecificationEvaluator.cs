using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Interfaces;
using SharedKernel.Domain.Specifications;
using System.Linq;

namespace PaymentService.Persistence;

public class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(
        IQueryable<T> inputQuery,
        ISpecification<T> specification, 
        bool ignorePaging = false)
    {
        IQueryable<T> query = inputQuery;

        if (specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy != null)
            query = query.OrderBy(specification.OrderBy);

        if (specification.OrderByDescending != null)
            query = query.OrderByDescending(specification.OrderByDescending);

        if (!ignorePaging)
        {
            if (specification.Skip.HasValue) query = query.Skip(specification.Skip.Value);
            if (specification.Take.HasValue) query = query.Take(specification.Take.Value);
        }

        return query;
    }
}
