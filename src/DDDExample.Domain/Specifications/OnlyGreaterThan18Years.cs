using System;
using System.Linq.Expressions;
using DDDExample.Domain.Models;
using DDDExample.SharedKernel.Specification;

namespace DDDExample.Domain.Specifications
{
    public class OnlyGreaterThan18Years : Specification<Customer>
    {
        public override Expression<Func<Customer, bool>> ToExpression() =>
            c => c.BirthDate.Year <= -18;
    }
}
