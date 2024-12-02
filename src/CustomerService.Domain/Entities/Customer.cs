using CustomerService.Domain.Enums;
using CustomerService.Domain.ValueObjects;
using EA.CommonLib.DomainObjects;

namespace CustomerService.Domain.Entities
{
    public class Customer : Entity, IAggregateRoot
    {
        public Customer(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            IsDeleted = false;
            CustomerType = ECustomerType.Customer;
        }

        protected Customer() { }
        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public Cpf Cpf { get; private set; } = null!;
        public bool IsDeleted { get; private set; }
        public ECustomerType CustomerType { get; private set; }
        public Address Address { get; private set; } = null!;
        public void ChangeEmail(string email) => Email = new Email(email);
        public void SetAddress(Address address) => Address = address;
        public void SetAsDeleted() => IsDeleted = true;
    }
}
