using EA.CommonLib.DomainObjects;

namespace CustomerService.Domain.Entities
{
    public class Address : Entity
    {
        public Address(string street, string number, string additionalInfo,
                       string neighborhood, string zipCode, string city,
                       string state, Guid customerId)
        {
            Street = street;
            Number = number;
            AdditionalInfo = additionalInfo;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            City = city;
            State = state;
            CustomerId = customerId;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string AdditionalInfo { get; private set; }
        public string Neighborhood { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; } = null!;
    }
}
