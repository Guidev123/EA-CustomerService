using CustomerService.Application.Events.CreatedCustomer;
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
                return new Response<CreateCustomerCommand>(request, 400, "Error", GetAllErrors(request.ValidationResult!));
            }

            var customer = CustomerMappers.MapToCustomer(request);

            var customerExists = await _customerRepository.GetByCpfAsync(customer.Cpf.Number);
            if(customerExists is not null)
            {
                AddError(request.ValidationResult!, "Customer already exists");
                return new Response<CreateCustomerCommand>(request, 400, "Error", GetAllErrors(request.ValidationResult!));
            }

            customer.AddEvent(new CreatedCustomerEvent(customer.Name, customer.Email.Address));

            await _customerRepository.CreateAsync(customer);

            var persistData = await _customerRepository.UnitOfWork.CommitAsync();
            if (!persistData)
            {
                AddError(request.ValidationResult!, "Fail to persist data");
                return new Response<CreateCustomerCommand>(request, 400, "Error", GetAllErrors(request.ValidationResult!));
            }

            return new Response<CreateCustomerCommand>(request, 201, "Success");
        }
    }
}
