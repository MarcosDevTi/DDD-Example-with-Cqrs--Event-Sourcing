using System;
using DDDExample.Cqrs.Query.Customer.Models;
using DDDExample.SharedKernel.Cqrs.Query;

namespace DDDExample.Cqrs.Query.Customer.Queries
{
    public class GetCustomerDetails : IQuery<CustomerDetails>
    {
        public GetCustomerDetails(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

    }
}
