using System;
using DDDExample.Domain.Core;

namespace DDDExample.Domain.Models
{
    public class Product : Entity
    {
        public Product()
        {

        }
        public Product(string name, string description, decimal price, Guid? id = null)
        {
            Id = id == null ? Guid.Empty : Id;
            Name = name;
            Description = description;
            Price = price;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
    }
}
