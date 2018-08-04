using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DDDExample.Cqrs.Query.Product.Models;
using DDDExample.Cqrs.Query.Product.Queries;
using DDDExample.Data.Context;
using DDDExample.SharedKernel.Cqrs.Query;

namespace DDDExample.Data.Hanlders.Product
{
    public class ProductQueryHandler :
        IQueryHandler<GetProductsIndex, IReadOnlyList<ProductIndex>>,
        IQueryHandler<GetProductDetails, ProductDetails>
    {
        private readonly DddExampleContext _architectureContext;

        public ProductQueryHandler(DddExampleContext architectureContext)
        {
            _architectureContext = architectureContext;
        }

        public IReadOnlyList<ProductIndex> Handle(GetProductsIndex query)
        {
            return _architectureContext.Products
                .ProjectTo<ProductIndex>().ToList();
        }


        public ProductDetails Handle(GetProductDetails query)
        {
            return Mapper.Map<ProductDetails>(
                _architectureContext.Products.Find(query.Id));
        }
    }
}
