using System;
using AutoMapper;
using DDDExample.Cqrs.Command.Customer;
using DDDExample.SharedKernel.AutoMapper;

namespace DDDExample.Cqrs.Event.Customer
{
    public class CustomerUpdated : SharedKernel.Cqrs.Event.Event, IMapperConfig
    {
        public CustomerUpdated() { }
        public CustomerUpdated(Guid id, string firstName, string lastName, string email, DateTime birthDate, string street, string number, string city, string zipCode)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
            Street = street;
            Number = number;
            City = city;
            ZipCode = zipCode;
            AggregateId = id;
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public void Map(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UpdateCustomer, Domain.Models.Customer>()
                .ConstructUsing(x => new Domain.Models.Customer(
                x.FirstName,
                    x.LastName,
                    x.Email,
                    x.BirthDate,
                    x.Street,
                    x.Number,
                    x.City,
                    x.ZipCode,
                    x.Id))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
