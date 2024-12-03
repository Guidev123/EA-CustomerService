using CustomerService.Application.Commands.AddAddress;
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
                return new Response<CreateCustomerCommand>(request, 400, GetAllErrors(request.ValidationResult!));
            }

            var customer = CustomerMappers.MapToCustomer(request);

            var customerExists = await _customerRepository.GetByCpfAsync(customer.Cpf.Number);
            if(customerExists is not null)
            {
                AddError(request.ValidationResult!, "Customer already exists");
                return new Response<CreateCustomerCommand>(request, 400, GetAllErrors(request.ValidationResult!));
            }

            await _customerRepository.CreateAsync(customer);

            return new Response<CreateCustomerCommand>(request, 201, "Success");
        }
    }
}
