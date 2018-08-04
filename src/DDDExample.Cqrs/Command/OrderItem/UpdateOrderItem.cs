using System;

namespace DDDExample.Cqrs.Command.OrderItem
{
    public class UpdateOrderItem
    {
        public UpdateOrderItem(Guid id, Guid productId, int qtd)
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
