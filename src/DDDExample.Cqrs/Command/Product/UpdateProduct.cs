using System;
using AutoMapper;
using DDDExample.Cqrs.Command.Product.Validation;
using DDDExample.SharedKernel.AutoMapper;

namespace DDDExample.Cqrs.Command.Product
{
    public class UpdateProduct : ProductCommand, IMapperConfig
    {
        public UpdateProduct() { }
        protected UpdateProduct(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProductValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void Map(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UpdateProduct, Domain.Models.Product>()
                .ConstructUsing(x => new Domain.Models.Product(x.Name, x.Description, x.Price, x.Id));
        }
    }
}
