using DDDExample.Domain.Core;

namespace DDDExample.Domain.Models
{
    public class OrderItem : Entity
    {
        public OrderItem()
        {


        }
        public OrderItem(Product product)
        {
            Product = product;
            Qtd = 1;
        }

        public Product Product { get; private set; }
        public int Qtd { get; set; }
    }
}
