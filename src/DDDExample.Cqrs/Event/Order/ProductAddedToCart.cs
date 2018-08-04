using System;

namespace DDDExample.Cqrs.Event.Order
{
    public class ProductAddedToCart : SharedKernel.Cqrs.Event.Event
    {
        public ProductAddedToCart(Guid orderId, Guid orderItemId, Guid productId)
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
            ProductId = productId;
            AggregateId = orderItemId;
        }

        public Guid OrderId { get; private set; }
        public Guid OrderItemId { get; private set; }
        public Guid ProductId { get; private set; }
    }
}
