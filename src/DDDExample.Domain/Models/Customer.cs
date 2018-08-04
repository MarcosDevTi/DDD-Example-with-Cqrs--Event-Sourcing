using System;
using System.Collections.Generic;
using DDDExample.Domain.Core;
using DDDExample.Domain.ValueObjects;

namespace DDDExample.Domain.Models
{
    public class Customer : Entity
    {
        protected Customer() { } // Empty constructor for EF
        public Customer(
            string firstName,
            string lastName,
            string email,
            DateTime birthDate,
            string street,
            string number,
            string city,
            string zipCode,
            Guid? id = null)
        {
            Id = id.Value ;
            Name = new Name(firstName, lastName);
            Email = new Email(email);
            BirthDate = birthDate;
            Address = new Address(street, number, city, zipCode);
            Orders = new List<Order>();
        }

        public void OrdersAdd(Order order) => Orders.Add(order);

        public void OrdersAdd(IEnumerable<Order> orders)
        {
            foreach (var order in orders) Orders.Add(order);
        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Address Address { get; private set; }
        public ICollection<Order> Orders { get; private set; }
    }
}
