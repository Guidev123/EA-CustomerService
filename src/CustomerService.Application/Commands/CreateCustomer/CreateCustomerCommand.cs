using EA.CommonLib.Messages;
using MediatR;

namespace CustomerService.Application.Commands.CreateCustomer
{
    public class CreateCustomerCommand : Command
    {
        public CreateCustomerCommand(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public override bool IsValid() => new CreateCustomerValidation().Validate(this).IsValid;
    }
}
