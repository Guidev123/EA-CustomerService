using CustomerService.Application.Events.CreatedCustomer;
using CustomerService.Application.Extensions;
using CustomerService.Application.Helpers;
using CustomerService.Application.Mappers;
using CustomerService.Domain.Repositories;
using EA.CommonLib.Messages;
using EA.CommonLib.Responses;
using MediatR;

namespace CustomerService.Application.Commands.CreateCustomer
{
    public class CreateCustomerHandler(ICustomerRepository customerRepository)
               : CommandHandler, IRequestHandler<CreateCustomerCommand, Response<CreateCustomerCommand>>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        public async Task<Response<CreateCustomerCommand>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return new Response<CreateCustomerCommand>(request, 400, ErrorMessages.SUCCESS.GetDescription(), GetAllErrors(request.ValidationResult!));
            }

            var customer = request.MapToCustomer();

            var customerExists = await _customerRepository.GetByCpfAsync(customer.Cpf.Number);
            if(customerExists is not null)
            {
                AddError(request.ValidationResult, ErrorMessages.CUSTOMER_ALREADY_EXISTS.GetDescription());
                return new Response<CreateCustomerCommand>(request, 400, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult!));
            }

            customer.AddEvent(new CreatedCustomerEvent(customer.Name, customer.Email.Address));

            await _customerRepository.CreateAsync(customer);

            var persistData = await _customerRepository.UnitOfWork.CommitAsync();
            if (!persistData)
            {
                AddError(request.ValidationResult, ErrorMessages.FAIL_PERSIST_DATA.GetDescription());
                return new Response<CreateCustomerCommand>(request, 400, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult!));
            }

            return new Response<CreateCustomerCommand>(request, 201, ErrorMessages.SUCCESS.GetDescription());
        }
    }
}
