namespace DDDExample.Cqrs.Command.Product.Validation
{
    public class DeleteProductValidation : ProductCommandValidation<DeleteProduct>
    {
        public DeleteProductValidation()
        {
            ValidateId();
        }
    }
}
