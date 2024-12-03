using EA.CommonLib.Messages;

namespace CustomerService.Application.Events.CreatedCustomer
{
    public class CreatedCustomerEvent : Event
    {
        public CreatedCustomerEvent(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}
