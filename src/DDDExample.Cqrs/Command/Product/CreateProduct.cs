using DDDExample.Cqrs.Command.Product.Validation;

namespace DDDExample.Cqrs.Command.Product
{
    public class CreateProduct : ProductCommand
    {
        public CreateProduct(){}

        protected CreateProduct(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateProductValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
