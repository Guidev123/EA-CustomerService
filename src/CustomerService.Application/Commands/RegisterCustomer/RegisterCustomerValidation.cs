using CustomerService.Domain.ValueObjects;
using FluentValidation;

namespace CustomerService.Application.Commands.RegisterCustomer
{
    public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerValidation()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Customer Id is invalid");

            RuleFor(x => x.Name).NotEmpty().WithMessage("The name can not be empty");

            RuleFor(x => x.Cpf).Must(ValidCpf).WithMessage("Cpf is invalid");

            RuleFor(x => x.Email).Must(ValidCpf).WithMessage("Email is invalid");
        }

        protected static bool ValidCpf(string cpf) => Cpf.Validate(cpf);
        protected static bool ValidEmail(string email) => Email.Validate(email);
    }
}
