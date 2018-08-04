using System.Collections.Generic;
using System.Linq;
using DDDExample.Cqrs.Query.OrderItem.Models;
using DDDExample.Cqrs.Query.OrderItem.Queries;
using DDDExample.Data.Context;
using DDDExample.Domain.Core;
using DDDExample.SharedKernel.Cqrs.Query;
using Microsoft.EntityFrameworkCore;

namespace DDDExample.Data.Hanlders.OderItem
{
    public class OderItemQueryHandler :
        IQueryHandler<GetOrderItensIndex, IReadOnlyList<OrderItemIndex>>,
        IQueryHandler<GetCart, Cqrs.Query.OrderItem.Models.Cart>
    {
        private readonly DddExampleContext _architectureContext;
        private readonly IUser _user;

        public OderItemQueryHandler(DddExampleContext architectureContext, IUser user)
        {
            _architectureContext = architectureContext;
            _user = user;
        }

        public IReadOnlyList<OrderItemIndex> Handle(GetOrderItensIndex query)
        {
            var ordersItens = from oi in _architectureContext.Orders
                    .Include(x => x.Customer)
                    .Include(x => x.OrderItems)
                              from o in oi.OrderItems
                              where oi.Customer.Id == _user.UserId()
                              select o;

            return ordersItens.Include(x => x.Product)
                .Select(x => new OrderItemIndex
                {
                    Id = x.Id,
                    Product = $"{x.Product.Name}, Price:{x.Product.Price}",
                    Qtd = x.Qtd
                }).ToList();
        }

        public Cqrs.Query.OrderItem.Models.Cart Handle(GetCart query)
        {
            var ordersItens = from oi in _architectureContext.Orders
                    .Include(x => x.Customer)
                    .Include(x => x.OrderItems)
                              from o in oi.OrderItems
                              where oi.Customer.Id == _user.UserId()
                              select o;

            var result = ordersItens.Include(x => x.Product)
                .Select(x => new OrderItemIndex
                {
                    Id = x.Id,
                    Product = $"{x.Product.Name}, Price:{x.Product.Price}",
                    Qtd = x.Qtd
                }).ToList();

            return new Cqrs.Query.OrderItem.Models.Cart
            {
                OrderItens = result,
                TotalPrice = ordersItens.Sum(x => x.Product.Price * x.Qtd)
            };
        }
    }
}
