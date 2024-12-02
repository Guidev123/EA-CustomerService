using CustomerService.Domain.Entities;

namespace CustomerService.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();  
        Task<Customer?> GetByCpfAsync(string cpf);
        Task<Customer?> GetByIdAsync(Guid id);
        Task CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
    }
}
