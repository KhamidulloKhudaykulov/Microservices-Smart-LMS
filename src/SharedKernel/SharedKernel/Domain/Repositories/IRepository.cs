using SharedKernel.Domain.Specifications;

namespace SharedKernel.Domain.Repositories;

public interface IRepository<T>
{
    Task<T> InsertAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> SelectByIdAsync(Guid id);

    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
    Task<int> CountAsync(ISpecification<T> specification);
}
