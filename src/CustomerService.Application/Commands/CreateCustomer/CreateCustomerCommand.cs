using SharedLib.Domain.Messages;

namespace CustomerService.Application.Commands.CreateCustomer
{
    public class CreateCustomerCommand : Command<CreateCustomerCommand>
    {
        public CreateCustomerCommand(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
            AggregateId = id;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
