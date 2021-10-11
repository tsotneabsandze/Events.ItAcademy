using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CORE.Specifications
{
    public interface ISpecification<T>
    {
        public Expression<Func<T, bool>> Filter { get; }
        public List<Expression<Func<T, bool>>> Filters { get; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
    }
}