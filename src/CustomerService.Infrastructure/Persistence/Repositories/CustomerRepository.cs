using CustomerService.Domain.Entities;
using CustomerService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository(CustomerDbContext context) : ICustomerRepository
    {
        private readonly CustomerDbContext _context = context;

        public async Task<List<Customer>> GetAllAsync() =>
            await _context.Customers.AsNoTracking().Where(x => !x.IsDeleted).ToListAsync();

        public async Task<Customer?> GetByCpfAsync(string cpf) =>
            await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Cpf.Number == cpf && !x.IsDeleted);

        public async Task<Customer?> GetByIdAsync(Guid id) =>
            await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public async Task CreateAsync(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task AddAddressAsync(Address address)
        {
            await _context.AddAsync(address);
            await _context.SaveChangesAsync();
        }
    }
}
