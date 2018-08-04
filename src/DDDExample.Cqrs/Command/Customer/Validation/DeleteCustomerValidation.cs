namespace DDDExample.Cqrs.Command.Customer.Validation
{
    public class DeleteCustomerValidation : CustomerCommandValidation<DeleteCustomer>
    {
        public DeleteCustomerValidation()
        {
            ValidateId();
        }
    }
}
