using System;
using DDDExample.Cqrs.Command.Product.Validation;

namespace DDDExample.Cqrs.Command.Product
{
    public class DeleteProduct : ProductCommand
    {
        public DeleteProduct(Guid id)
        {
            Id = id;
        }
        public override bool IsValid()
        {
            ValidationResult = new DeleteProductValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
