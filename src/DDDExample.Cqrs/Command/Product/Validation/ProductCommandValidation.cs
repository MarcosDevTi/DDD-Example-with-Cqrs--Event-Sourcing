using System;
using FluentValidation;

namespace DDDExample.Cqrs.Command.Product.Validation
{
    public class ProductCommandValidation<T> : AbstractValidator<T> where T : ProductCommand
    {
        protected void ValidateId() =>
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

        protected void ValidateName() =>
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please easure you have entered the First Name")
                .Length(2, 50).WithMessage("The Name must have between 2 and 50 characters");

        protected void ValidateDescription() =>
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Please easure you have entered the First Name")
                .Length(2, 50).WithMessage("The Name must have between 2 and 50 characters");

        protected void ValidatePrice() =>
            RuleFor(x => x.Price).GreaterThanOrEqualTo(x => 0)
                .WithMessage("The Price must have greater then 0");
    }
}
