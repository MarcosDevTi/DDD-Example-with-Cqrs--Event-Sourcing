using DDDExample.Cqrs.Query.OrderItem.Models;
using DDDExample.SharedKernel.Cqrs.Query;

namespace DDDExample.Cqrs.Query.OrderItem.Queries
{
    public class GetCart : IQuery<Cart>
    {
    }
}
