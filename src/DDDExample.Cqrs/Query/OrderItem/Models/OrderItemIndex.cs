using System;

namespace DDDExample.Cqrs.Query.OrderItem.Models
{
    public class OrderItemIndex
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public int Qtd { get; set; }
    }
}
