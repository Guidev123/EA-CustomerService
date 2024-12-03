using CustomerService.Application.Mappers;
using CustomerService.Domain.Repositories;
using EA.CommonLib.Messages;
using EA.CommonLib.Responses;
using MediatR;

namespace CustomerService.Application.Commands.AddAddress
{
    public class AddAddressHandler(ICustomerRepository customerRepository)
                                 : CommandHandler,
                                   IRequestHandler<AddAddressCommand, Response<AddAddressCommand>>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        public async Task<Response<AddAddressCommand>> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return new Response<AddAddressCommand>(request, 400, GetAllErrors(request.ValidationResult!));
            }

            var address = CustomerMappers.MapToAddress(request);

            var customerExists = await _customerRepository.GetByIdAsync(address.CustomerId);
            if (customerExists is null)
            {
                AddError(request.ValidationResult!, "Customer not found");
                return new Response<AddAddressCommand>(request, 400, GetAllErrors(request.ValidationResult!));
            }

            await _customerRepository.AddAddressAsync(address);

            return new Response<AddAddressCommand>(request, 201, "Success");
        }
    }
}
