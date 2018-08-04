using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DDDExample.Cqrs.Command.Customer;
using DDDExample.SharedKernel.AutoMapper;

namespace DDDExample.Cqrs.Query.Customer.Models
{
    public class CustomerDetails : IMapperConfig
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The E-mail is Required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The BirthDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }

        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }

        public void Map(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Domain.Models.Customer, CustomerDetails>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Name.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Name.LastName))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.EmailAddress))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(d => d.Number, o => o.MapFrom(s => s.Address.Number))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Address.City))
                .ForMember(d => d.ZipCode, o => o.MapFrom(s => s.Address.ZipCode));

            cfg.CreateMap<CustomerDetails, UpdateCustomer>()
                .ConstructUsing(x => new UpdateCustomer(
                        x.Id,
                        x.FirstName,
                        x.LastName,
                        x.Email,
                        x.BirthDate,
                        x.Street,
                        x.Number,
                        x.City,
                        x.ZipCode));
        }


    }
}
