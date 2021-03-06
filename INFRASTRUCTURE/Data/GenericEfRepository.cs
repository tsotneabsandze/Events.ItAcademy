using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Interfaces;
using CORE.Specifications;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Data
{
    public class GenericEfRepository<T> : IGenericRepository<T> where T : BaseEntity<int>
    {
        protected readonly AppDbContext Ctx;

        public GenericEfRepository(AppDbContext ctx)
        {
            Ctx = ctx;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
            => await Ctx.Set<T>().ToListAsync(cancellationToken);

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var keyVal = new object[] { id };
            return await Ctx.Set<T>().FindAsync(keyVal, cancellationToken);
        }

        public async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Ctx.Set<T>().AddAsync(entity, cancellationToken);
            await Ctx.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            Ctx.Entry(entity).State = EntityState.Modified;
            await Ctx.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            Ctx.Set<T>().Remove(entity);
            await Ctx.SaveChangesAsync(cancellationToken);
        }

        public async Task<T> GetItemWithSpecAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
            => await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);


        public async Task<IReadOnlyList<T>> ListBySpecAsync(ISpecification<T> spec,
            CancellationToken cancellationToken = default)
            => await ApplySpecification(spec).ToListAsync(cancellationToken: cancellationToken);


        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
            => await Ctx.Set<T>().CountAsync(cancellationToken);


        public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
            => await ApplySpecification(spec).CountAsync(cancellationToken);


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            IQueryable<T> baseQuery = Ctx.Set<T>();
            return SpecificationEvaluator<T>.GetQuery(baseQuery, spec);
        }
    }
}