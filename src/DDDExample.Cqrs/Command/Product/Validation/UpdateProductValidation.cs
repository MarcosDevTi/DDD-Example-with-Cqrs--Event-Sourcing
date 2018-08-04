namespace DDDExample.Cqrs.Command.Product.Validation
{
    public class UpdateProductValidation : ProductCommandValidation<UpdateProduct>
    {
        public UpdateProductValidation()
        {
            ValidateId();
            ValidateName();
            ValidateDescription();
            ValidatePrice();
        }
    }
}
