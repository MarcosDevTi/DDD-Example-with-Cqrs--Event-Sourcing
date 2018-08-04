using System;

namespace DDDExample.Cqrs.Event.OrderItem
{
    public class OrderItemCreated : SharedKernel.Cqrs.Event.Event
    {
        public OrderItemCreated(Guid id, Guid productId, int qtd)
        {
            Id = id;
            ProductId = productId;
            Qtd = qtd;
        }
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Qtd { get; private set; }
        
    }
}
