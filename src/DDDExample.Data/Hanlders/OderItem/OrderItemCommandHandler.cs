using System.Linq;
using DDDExample.Cqrs.Command.OrderItem;
using DDDExample.Data.Context;
using DDDExample.Domain.Models;
using DDDExample.SharedKernel.Cqrs.Command;
using Microsoft.EntityFrameworkCore;

namespace DDDExample.Data.Hanlders.OderItem
{
    public class OrderItemCommandHandler :
        ICommandHandler<CreateOrderItem>
    {
        private readonly DddExampleContext _architectureContext;

        public OrderItemCommandHandler(DddExampleContext architectureContext)
        {
            _architectureContext = architectureContext;
        }

        public void Handle(CreateOrderItem command)
        {
            var product = _architectureContext.Products.Find(command.ProductId);

            var orderItem = _architectureContext.OrderItems.Include(x => x.Product)
                .FirstOrDefault(x => x.Product.Id == command.ProductId);

            if (orderItem == null)
            {
                _architectureContext.OrderItems.Add(new OrderItem(product));
            }
            else
            {
                orderItem.Qtd++;
                _architectureContext.OrderItems.Update(orderItem);
            }
            
            _architectureContext.SaveChanges();
        }
    }
}
