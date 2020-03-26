using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using GraphQl.Core.Interfaces.Specifications;

namespace GraphQl.Core.Specifications
{
    public class SpecificationBase<T> : ISpecification<T>
    {
        protected SpecificationBase(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludesStrings { get; } = new List<string>();

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includesString)
        {
            IncludesStrings.Add(includesString);
        }
    }
}
