using System;

namespace DDDExample.Cqrs.Command.OrderItem
{
    public class DeleteOrderItem : SharedKernel.Cqrs.Command.Command
    {
        public DeleteOrderItem(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public override bool IsValid()
        {
            return true;
        }
    }
}
