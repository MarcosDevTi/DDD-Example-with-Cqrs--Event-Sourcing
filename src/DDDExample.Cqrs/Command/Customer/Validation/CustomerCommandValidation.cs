using System;
using FluentValidation;

namespace DDDExample.Cqrs.Command.Customer.Validation
{
    public class CustomerCommandValidation<T> : AbstractValidator<T> where T : CustomerCommand
    {
        protected void ValidateId() =>
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

        protected void ValidateFirstName() =>
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Please easure you have entered the First Name")
                .Length(2, 40).WithMessage("The First Name must have between 2 and 40 characters");

        protected void ValidateLastName() =>
            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Please ensure you have entered the Last Name")
                .Length(2, 120).WithMessage("The Last Name must have between 2 and 120 characters");

        protected void ValidateBirthDate() =>
            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("The customer must have 18 years or more");

        protected bool HaveMinimumAge(DateTime birthDate) =>
            birthDate <= DateTime.Now.AddYears(-18);

        protected void ValidateEmail() =>
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
    }
}
