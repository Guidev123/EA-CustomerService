using CustomerService.Application.Extensions;
using CustomerService.Application.Helpers;
using CustomerService.Application.Mappers;
using CustomerService.Application.Services;
using CustomerService.Domain.Repositories;
using MediatR;
using SharedLib.Domain.Messages;
using SharedLib.Domain.Responses;

namespace CustomerService.Application.Commands.AddAddress
{
    public class AddAddressHandler(ICustomerRepository customerRepository, IUserService userService)
                                 : CommandHandler,
                                   IRequestHandler<AddAddressCommand, Response<AddAddressCommand>>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IUserService _userService = userService;
        public async Task<Response<AddAddressCommand>> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return new(request, 400, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult));
            var customerId = _userService.GetUserId();
            if (!customerId.HasValue || customerId is null)
                return new(null, 400, ErrorMessages.CUSTOMER_NOT_FOUND.GetDescription());

            var address = request.MapToAddress(customerId.Value);

            var customerExists = await _customerRepository.GetByIdAsync(address.CustomerId);
            if (customerExists is null)
            {
                AddError(request.ValidationResult, ErrorMessages.CUSTOMER_NOT_FOUND.GetDescription());
                return new(request, 400, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult));
            }

            await _customerRepository.AddAddressAsync(address);

            var persistData = await _customerRepository.UnitOfWork.CommitAsync();
            if (!persistData)
            {
                AddError(request.ValidationResult, ErrorMessages.FAIL_PERSIST_DATA.GetDescription());
                return new(request, 400, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult));
            }

            return new(request, 201, ErrorMessages.SUCCESS.GetDescription());
        }
    }
}
