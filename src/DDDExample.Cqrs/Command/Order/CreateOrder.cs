using System;

namespace DDDExample.Cqrs.Command.Order
{
    public class CreateOrder
    {
        public Guid CustomerId { get; set; }
    }
}
