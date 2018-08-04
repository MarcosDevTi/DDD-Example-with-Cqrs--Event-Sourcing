namespace DDDExample.Cqrs.Command.Customer.Validation
{
    public class CreateCustomerValidation : CustomerCommandValidation<CreateCustomer>
    {
        public CreateCustomerValidation()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
