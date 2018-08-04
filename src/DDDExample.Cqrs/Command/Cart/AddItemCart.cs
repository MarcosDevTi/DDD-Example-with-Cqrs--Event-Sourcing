using System;

namespace DDDExample.Cqrs.Command.Cart
{
    public class AddItemCart : SharedKernel.Cqrs.Command.Command
    {
        public AddItemCart(Guid orderItemId, int value)
        {
            Value = value;
            OrderItemId = orderItemId;
        }

        public int Value { get; private set; }

        public Guid OrderItemId { get; private set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
