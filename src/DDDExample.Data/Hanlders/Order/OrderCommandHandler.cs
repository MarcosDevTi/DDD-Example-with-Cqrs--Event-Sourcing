using System.Linq;
using DDDExample.Cqrs.Command.Order;
using DDDExample.Cqrs.Event.Order;
using DDDExample.Data.Context;
using DDDExample.Domain.Core;
using DDDExample.Domain.Core.DomainNotifications;
using DDDExample.Domain.Event;
using DDDExample.Domain.Models;
using DDDExample.SharedKernel.Cqrs.Command;
using Microsoft.EntityFrameworkCore;

namespace DDDExample.Data.Hanlders.Order
{
    public class OrderCommandHandler : CommandHandler<Domain.Models.Order>,
        ICommandHandler<AddProductToCart>
    {
        private readonly DddExampleContext _architectureContext;
        private readonly IUser _user;

        public OrderCommandHandler(DddExampleContext architectureContext, IDomainNotification notifications, IEventRepository eventRepository, IUser user) : base(architectureContext, notifications, eventRepository)
        {
            _architectureContext = architectureContext;
            _user = user;
        }
        public void Handle(AddProductToCart command)
        {
            var product = _architectureContext.Products.Find(command.ProductId);

            var order = _architectureContext.Orders
                     .Include(x => x.OrderItems).ThenInclude(c => c.Product)
                     .FirstOrDefault(x => x.Customer.Id == _user.UserId() && x.Closed == false);

            if (order == null)
            {
                var customer = _architectureContext.Customers.Find(command.UserId);

                order = new Domain.Models.Order(customer);
                var orderItem = new OrderItem(product);
                order.OrderItems.Add(orderItem);
                Db().Add(order);
                Commit(new ProductAddedToCart(order.Id, orderItem.Id, command.ProductId));
            }
            else
            {
                var orderItem = order.OrderItems.FirstOrDefault(x => x.Product.Id == command.ProductId);
                order.AddOrUpdateItem(orderItem, product);

                Db().Update(order);
                Commit(new ProductAddedToCart(order.Id, order.Id, command.ProductId));
            }
        }

    }
}
