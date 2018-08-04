using System;

namespace DDDExample.Cqrs.Command.OrderItem
{
    public class CreateOrderItem : SharedKernel.Cqrs.Command.Command
    {
        public CreateOrderItem()
        {
            
        }
        public CreateOrderItem(Guid productId, int qtd)
        {
            ProductId = productId;
            Qtd = qtd;
        }
        public Guid ProductId { get; set; }
        public int Qtd { get; set; }
        public override bool IsValid()
        {
            return true;
        }
    }
}
