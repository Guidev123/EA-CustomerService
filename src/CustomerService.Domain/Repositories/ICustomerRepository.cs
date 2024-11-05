using CustomerService.Domain.Entities;

namespace CustomerService.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateCustomer(Customer customer);
        Task<List<Customer>> GetAll();  
        Task<Customer?> GetByCpf(string cpf);
    }
}
