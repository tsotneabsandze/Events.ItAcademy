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

        public List<Expression<Func<T, bool>>> Filters { get; } =
            new List<Expression<Func<T, bool>>>();

        public List<Expression<Func<T, object>>> Includes { get; set; } =
            new List<Expression<Func<T, object>>>();

        protected void AddFilters(List<Expression<Func<T, bool>>> filters,
            Expression<Func<T, bool>> filterToAdd)
        {
            filters.Add(filterToAdd);
        }

        protected void AddInclude(List<Func<T, object>> includes,
            Expression<Func<T, object>> expressionToInclude)
        {
            Includes.Add(expressionToInclude);
        }
    }
}