using CustomerService.Application.Extensions;
using CustomerService.Application.Helpers;
using CustomerService.Application.Mappers;
using CustomerService.Domain.Repositories;
using MediatR;
using SharedLib.Domain.Messages;
using SharedLib.Domain.Responses;

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
                return new Response<AddAddressCommand>(request, 400, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult));
            }

            var address = request.MapToAddress();

            var customerExists = await _customerRepository.GetByIdAsync(address.CustomerId);
            if (customerExists is null)
            {
                AddError(request.ValidationResult, ErrorMessages.CUSTOMER_NOT_FOUND.GetDescription());
                return new Response<AddAddressCommand>(request, 400, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult));
            }

            await _customerRepository.AddAddressAsync(address);

            var persistData = await _customerRepository.UnitOfWork.CommitAsync();
            if (!persistData)
            {
                AddError(request.ValidationResult, ErrorMessages.FAIL_PERSIST_DATA.GetDescription());
                return new Response<AddAddressCommand>(request, 400, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult));
            }

            return new Response<AddAddressCommand>(request, 201, ErrorMessages.SUCCESS.GetDescription());
        }
    }
}
