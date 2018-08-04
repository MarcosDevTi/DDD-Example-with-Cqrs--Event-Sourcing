using System.Collections.Generic;
using DDDExample.Cqrs.Query.OrderItem.Models;
using DDDExample.SharedKernel.Cqrs.Query;

namespace DDDExample.Cqrs.Query.OrderItem.Queries
{
    public class GetOrderItensIndex : IQuery<IReadOnlyList<OrderItemIndex>>
    {
    }
}
