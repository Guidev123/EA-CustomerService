using CustomerService.Domain.Entities;
using EA.CommonLib.Messages;

namespace CustomerService.Application.Commands.AddAddress
{
    public class AddAddressCommand : Command<AddAddressCommand>
    {
        public AddAddressCommand(Guid customerId, string street, string number,
                                 string additionalInfo, string neighborhood,
                                 string zipCode, string state, string city)
        {
            AggregateId = Guid.NewGuid();
            CustomerId = customerId;
            Street = street;
            Number = number;
            AdditionalInfo = additionalInfo;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            State = state;
            City = city;
        }
        public Guid CustomerId { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string AdditionalInfo { get; private set; }
        public string Neighborhood { get; private set; }
        public string ZipCode { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public override bool IsValid()
        {
            ValidationResult = new AddAddressValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
