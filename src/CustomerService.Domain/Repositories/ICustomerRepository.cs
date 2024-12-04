using CustomerService.Domain.Entities;

namespace CustomerService.Domain.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        Task<List<Customer>> GetAllAsync();  
        Task<Customer?> GetByCpfAsync(string cpf);
        Task<Customer?> GetByIdAsync(Guid id);
        Task CreateAsync(Customer customer);
        Task AddAddressAsync(Address address);
        void UpdateAsync(Customer customer);
        IUnitOfWork UnitOfWork { get; }
    }
}
