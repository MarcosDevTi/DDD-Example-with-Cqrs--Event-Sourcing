using System;
using System.Linq;
using System.Linq.Expressions;
using DDDExample.Domain.Models;
using DDDExample.SharedKernel.Specification;

namespace DDDExample.Domain.Specifications
{
    public class SearchCustomer : Specification<Customer>
    {
        public string Criteria { get; set; }
        public override Expression<Func<Customer, bool>> ToExpression()
        {
            return c => Criteria.Any(x =>
               c.Name.FirstName.Contains(x) ||
               c.Name.LastName.Contains(x) ||
               c.Email.EmailAddress.Contains(x));
        }

        public SearchCustomer AddSearch(string search)
        {
            Criteria = search;
            return this;
        }
    }
}
