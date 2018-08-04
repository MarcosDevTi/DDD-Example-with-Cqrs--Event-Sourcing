using AutoMapper;
using DDDExample.Cqrs.Query.Customer.Models;
using DDDExample.Cqrs.Query.Customer.Queries;
using DDDExample.Data.Context;
using DDDExample.SharedKernel.Cqrs.Query;
using DDDExample.SharedKernel.Paging;

namespace DDDExample.Data.Hanlders.Customer
{
    public class CustomerQueryHandler :
        IQueryHandler<GetCustomersIndex, PagedResult<CustomerIndex>>,
        IQueryHandler<GetCustomerDetails, CustomerDetails>
    {
        private readonly DddExampleContext _architectureContext;

        public CustomerQueryHandler(DddExampleContext architectureContext)
        {
            _architectureContext = architectureContext;
        }

        public PagedResult<CustomerIndex> Handle(GetCustomersIndex query)

        {
            return _architectureContext.Customers
                //.Where(
                    //new SpecGeneric<Domain.Models.Customer>()
                        //.AddSpec(new OnlyGreaterThan18Years())
                        //.AddSpec(new EspecialCustomer())
                        //.AddSpec(new SearchCustomer().AddSearch(query.Search))
                        //.Build())
                .GetPagedResult(
                query.Paging);
        }

        public CustomerDetails Handle(GetCustomerDetails query)
        {
            return Mapper.Map<CustomerDetails>(_architectureContext.Customers.Find(query.Id));
        }
    }
}
