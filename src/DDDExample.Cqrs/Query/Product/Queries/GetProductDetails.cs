using System;
using DDDExample.Cqrs.Query.Product.Models;
using DDDExample.SharedKernel.Cqrs.Query;

namespace DDDExample.Cqrs.Query.Product.Queries
{
    public class GetProductDetails : IQuery<ProductDetails>
    {
        public GetProductDetails(Guid? id)
        {
            Id = id;
        }

        public Guid? Id { get; private set; }
    }
}
