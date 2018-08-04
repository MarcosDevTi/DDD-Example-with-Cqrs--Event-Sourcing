using System;
using System.Linq.Expressions;
using DDDExample.Domain.Models;
using DDDExample.SharedKernel.Specification;

namespace DDDExample.Domain.Specifications
{
    public class EspecialCustomer : Specification<Customer>
    {
        public override Expression<Func<Customer, bool>> ToExpression()
        {
            return c => c.Orders.Count > 10;
        }
    }
}
