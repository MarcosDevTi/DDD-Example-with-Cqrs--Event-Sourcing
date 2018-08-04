using System;
using DDDExample.SharedKernel.AutoMapper;

namespace DDDExample.Cqrs.Query.Product.Models
{
    public class ProductIndex : IMapFrom<Domain.Models.Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
