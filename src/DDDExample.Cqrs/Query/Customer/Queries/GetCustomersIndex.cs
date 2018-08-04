using DDDExample.Cqrs.Query.Customer.Models;
using DDDExample.SharedKernel.Cqrs.Query;
using DDDExample.SharedKernel.Paging;

namespace DDDExample.Cqrs.Query.Customer.Queries
{
    public class GetCustomersIndex : IQuery<PagedResult<CustomerIndex>>
    {
        public GetCustomersIndex(
            Paging<CustomerIndex> paging,
            string search)
        {
            Paging = paging;
            Search = search ?? "";
        }

        public Paging<CustomerIndex> Paging { get; private set; }
        public string Search { get; private set; }
    }
}
