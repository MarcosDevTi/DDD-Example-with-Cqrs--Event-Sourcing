using System;
using AutoMapper;
using DDDExample.SharedKernel.AutoMapper;

namespace DDDExample.Cqrs.Query.Customer.Models
{
    public class CustomerIndex : IMapperConfig

    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public bool Especial { get; set; }
        public void Map(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Domain.Models.Customer, CustomerIndex>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Name.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Name.LastName))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.EmailAddress))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(d => d.Number, o => o.MapFrom(s => s.Address.Number))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Address.City))
                .ForMember(d => d.ZipCode, o => o.MapFrom(s => s.Address.ZipCode));

            cfg.CreateMap<CustomerIndex, Domain.Models.Customer>()
                .ConstructUsing(x => new Domain.Models.Customer(
                    x.FirstName, x.LastName, x.Email, x.BirthDate, x.Street,
                    x.Number, x.City, x.ZipCode));

        }
    }
}
