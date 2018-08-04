namespace DDDExample.Cqrs.Command.Customer.Validation
{
    public class UpdateCustomerValidation : CustomerCommandValidation<UpdateCustomer>
    {
        public UpdateCustomerValidation()
        {
            ValidateId();
            ValidateFirstName();
            ValidateLastName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
