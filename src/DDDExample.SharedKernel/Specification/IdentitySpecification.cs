using System;
using System.Linq.Expressions;

namespace DDDExample.SharedKernel.Specification
{
    public sealed class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }
}