using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CORE.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        protected BaseSpecification()
        {
        }

        protected BaseSpecification(Expression<Func<T, bool>> filter)
        {
            Filter = filter;
        }

        public Expression<Func<T, bool>> Filter { get; }

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }
        

        public List<Expression<Func<T, object>>> Includes { get; set; } =
            new List<Expression<Func<T, object>>>();


        protected void AddFilters(List<Expression<Func<T, bool>>> filters,
            Expression<Func<T, bool>> filterToAdd)
        {
            filters.Add(filterToAdd);
        }
        

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}