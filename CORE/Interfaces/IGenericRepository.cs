using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Specifications;

namespace CORE.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity<int>
    {
        Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> GetItemWithSpecAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> ListBySpecAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
    }
}