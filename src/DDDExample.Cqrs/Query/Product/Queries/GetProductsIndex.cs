using System.Collections.Generic;
using DDDExample.Cqrs.Query.Product.Models;
using DDDExample.SharedKernel.Cqrs.Query;

namespace DDDExample.Cqrs.Query.Product.Queries
{
    public class GetProductsIndex : IQuery<IReadOnlyList<ProductIndex>>
    {

    }
}
