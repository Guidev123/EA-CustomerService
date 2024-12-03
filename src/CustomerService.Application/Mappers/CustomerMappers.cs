using CustomerService.Application.Commands.AddAddress;
using CustomerService.Application.Commands.CreateCustomer;
using CustomerService.Domain.Entities;

namespace CustomerService.Application.Mappers
{
    public static class CustomerMappers
    {
        public static Customer MapToCustomer(CreateCustomerCommand command) =>
            new(command.Id, command.Name, command.Email, command.Cpf);

        public static Address MapToAddress(AddAddressCommand command) =>
            new(command.CustomerId, command.Street, command.Number, command.AdditionalInfo,
                command.Neighborhood, command.ZipCode, command.City,
                command.State);

    }
}
