using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CORE.Entities;

namespace CORE.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity<int>
    {
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}