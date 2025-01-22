using CustomerService.Domain.ValueObjects;
using FluentValidation;

namespace CustomerService.Application.Commands.Create
{
    public class CreateCustomerValidation : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer id invalid");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name can not be empty");

            RuleFor(c => c.Cpf)
                .Must(GetCpfValidation)
                .WithMessage("Invalid CPF");

            RuleFor(c => c.Email)
                .Must(GetEmailValidation)
                .WithMessage("Invalid E-mail");
        }

        protected static bool GetCpfValidation(string cpf) => Cpf.Validate(cpf);
        protected static bool GetEmailValidation(string email) => Email.Validate(email);
    }
}
