using System;
using AutoMapper;
using DDDExample.Cqrs.Command.Customer.Validation;
using DDDExample.SharedKernel.AutoMapper;

namespace DDDExample.Cqrs.Command.Customer
{
    public class CreateCustomer : CustomerCommand, IMapperConfig
    {
        //ctor AutoMapper
        public CreateCustomer() { }
        public CreateCustomer(
            string firstName,
            string lastName,
            string email,
            DateTime birthDate,
            string street,
            string number,
            string city,
            string zipCode, 
            Guid? userId = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
            Street = street;
            Number = number;
            City = city;
            ZipCode = zipCode;
            Id = userId;
        }

        public void Map(IMapperConfigurationExpression cfg) =>
            cfg.CreateMap<CreateCustomer, Domain.Models.Customer>()
                .ConstructUsing(c=> 
                    new Domain.Models.Customer(
                        c.FirstName, c.LastName, c.Email, c.BirthDate, c.Street, c.Number, c.City, c.ZipCode, c.Id))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

        public override bool IsValid()
        {
            ValidationResult = new CreateCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
