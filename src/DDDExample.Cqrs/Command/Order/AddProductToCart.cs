using System;

namespace DDDExample.Cqrs.Command.Order
{
    public class AddProductToCart : SharedKernel.Cqrs.Command.Command
    {
        public AddProductToCart(Guid productId, Guid userId)
        {
            ProductId = productId;
            UserId = userId;
        }

        public Guid ProductId { get; private set; }
        public Guid UserId { get; private set; }
        public override bool IsValid()
        {
            return true;
        }
    }
}
