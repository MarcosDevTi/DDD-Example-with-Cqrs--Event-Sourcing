using System;
using DDDExample.Cqrs.Command.Customer.Validation;

namespace DDDExample.Cqrs.Command.Customer
{
    public class DeleteCustomer : CustomerCommand
    {
        public DeleteCustomer(Guid id)
        {
            Id = id;
        }
        public override bool IsValid()
        {
            ValidationResult = new DeleteCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
