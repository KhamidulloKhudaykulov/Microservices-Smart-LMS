using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Specifications;

namespace LessonModule.Infrastructure.Repositories;

public static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(
        IQueryable<T> inputQuery,
        ISpecification<T> specification)
        where T : class
    {
        var query = inputQuery;

        if (specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        query = specification.Includes.Aggregate(query,
            (current, include) => current.Include(include));

        if (specification.OrderBy is not null)
            query = query.OrderBy(specification.OrderBy);
        else if (specification.OrderByDescending is not null)
            query = query.OrderByDescending(specification.OrderByDescending);

        if (specification.IsPagingEnabled ?? false)
            query = query.Skip(specification.Skip ?? 0).Take(specification.Take ?? 0);

        return query;
    }
}
