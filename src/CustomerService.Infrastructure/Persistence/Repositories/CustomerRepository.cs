using CustomerService.Domain.Entities;
using CustomerService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CustomerService.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository(CustomerDbContext context) : ICustomerRepository
    {
        private readonly CustomerDbContext _context = context;

        public async Task<List<Customer>> GetAllAsync() =>
            await _context.Customers.AsNoTracking().ToListAsync();

        public async Task<Customer?> GetByCpfAsync(string cpf) =>
            await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Cpf.Number == cpf);

        public async Task<Customer?> GetByIdAsync(Guid id) =>
            await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(Customer customer) =>  await _context.AddAsync(customer);
        public void UpdateAsync(Customer customer) => _context.Update(customer);
        public async Task AddAddressAsync(Address address) => await _context.AddAsync(address);

        public async Task<Address?> GetAddressAsync(Guid customerId)
            => await _context.Customers.Where(c => c.Id == customerId).Select(c => c.Address).FirstOrDefaultAsync();

        public IUnitOfWork UnitOfWork => _context;
    }
}
