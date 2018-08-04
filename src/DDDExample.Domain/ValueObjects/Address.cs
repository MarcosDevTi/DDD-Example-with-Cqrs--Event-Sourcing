namespace DDDExample.Domain.ValueObjects
{
    public class Address
    {
        public Address(string street, string number, string city, string zipCode)
        {
            Street = street;
            Number = number;
            City = city;
            ZipCode = zipCode;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
    }
}
