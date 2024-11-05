using CustomerService.Application.Messages;

namespace CustomerService.Application.Commands.RegisterCustomer
{
    public class RegisterCustomerCommand : Command
    {
        public RegisterCustomerCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public override bool IsValid() => new RegisterCustomerValidation().Validate(this).IsValid;
    }
}
