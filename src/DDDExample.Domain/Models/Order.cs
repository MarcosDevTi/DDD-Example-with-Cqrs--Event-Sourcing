using System.Collections.Generic;
using System.Linq;
using DDDExample.Domain.Core;

namespace DDDExample.Domain.Models
{
    public class Order : Entity
    {
        public Order()
        {

        }

        public Order(Customer customer)
        {
            Customer = customer;
            OrderItems = new List<OrderItem>();
        }

        public void AddOrUpdateItem(OrderItem orderItem, Product product)
        {
            if (orderItem == null)
                OrderItems.Add(new OrderItem(product));
            else
                OrderItems.Where(x => x.Product.Id == orderItem.Product.Id).Select(x => x.Qtd++).ToList();
        }

        public void CloseOrder(bool val) => Closed = val;

        public Customer Customer { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; }
        public bool Closed { get; private set; }
    }
}
