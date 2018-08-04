using System;
using DDDExample.Cqrs.Command.Product;
using DDDExample.SharedKernel.AutoMapper;

namespace DDDExample.Cqrs.Query.Product.Models
{
    public class ProductDetails : IMapFrom<Domain.Models.Product>, IMapTo<UpdateProduct>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
