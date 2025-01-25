using CustomerService.Application.Commands.AddAddress;
using CustomerService.Application.Commands.Create;
using CustomerService.Domain.Entities;

namespace CustomerService.Application.Mappers
{
    public static class CustomerMappers
    {
        public static Customer MapToCustomer(this CreateCustomerCommand command) =>
            new(command.Id, command.Name, command.Email, command.Cpf);

        public static Address MapToAddress(this AddAddressCommand command, Guid customerId) =>
            new(customerId, command.Street, command.Number, command.AdditionalInfo,
                command.Neighborhood, command.ZipCode, command.City,
                command.State);

    }
}
