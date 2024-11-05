using CustomerService.Application.Messages;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Repositories;
using FluentValidation.Results;
using MediatR;

namespace CustomerService.Application.Commands.RegisterCustomer
{   
    public class RegisterCustomerHandler(ICustomerRepository customerRepository) : Handler, IRequestHandler<RegisterCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        public async Task<ValidationResult> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            if(!request.IsValid()) return request.ValidationResult;

            var customer = new Customer(request.Id, request.Name, request.Email, request.Cpf);

            var customerExists = await _customerRepository.GetByCpf(customer.Cpf.Number);
            if(customerExists is not null)
            {
                AddError("Customer already exists");
                return ValidationResult;
            }

            await _customerRepository.CreateCustomer(customer!);

            return ValidationResult;
        }
    }
}
