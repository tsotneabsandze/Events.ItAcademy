using System.Linq;
using System.Net.Mime;
using CORE.Entities;
using CORE.Specifications;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity
        : BaseEntity<int>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity>
            baseQuery, ISpecification<TEntity> spec)
        {
            IQueryable<TEntity> query = baseQuery;

            if (spec.Filter != null)
                query = query.Where(spec.Filter);

            query = spec.Filters.Aggregate(query, (currentQuery, filter) =>
                currentQuery.Where(filter));

            query = spec.Includes.Aggregate(
                query, (currentState, includeExpr) =>
                    currentState.Include(includeExpr));

            return query;
        }
    }
}