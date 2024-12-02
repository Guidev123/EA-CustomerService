using CustomerService.Domain.Entities;
using CustomerService.Domain.Repositories;
using EA.CommonLib.Messages;
using FluentValidation.Results;
using MediatR;

namespace CustomerService.Application.Commands.CreateCustomer
{
    public class CreateCustomerHandler(ICustomerRepository customerRepository) : CommandHandler, IRequestHandler<CreateCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        public async Task<ValidationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if(!request.IsValid()) return request.ValidationResult;

            var customer = new Customer(request.Id, request.Name, request.Email, request.Cpf);
            var customerExists = await _customerRepository.GetByCpf(customer.Cpf.Number);
            if(customerExists is not null)
            {
                AddError("Customer already exists");
                return request.ValidationResult;
            }

            await _customerRepository.CreateCustomer(customer);

            return request.ValidationResult;
        }
    }
}
