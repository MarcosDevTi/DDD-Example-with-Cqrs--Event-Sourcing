using System;

namespace DDDExample.Cqrs.Event.OrderItem
{
    public class OrderItemDeleted : SharedKernel.Cqrs.Event.Event
    {
        public OrderItemDeleted(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
