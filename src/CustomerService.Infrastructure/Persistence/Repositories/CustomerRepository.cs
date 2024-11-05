using CustomerService.Domain.Entities;
using CustomerService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository(CustomerDbContext context) : ICustomerRepository
    {
        private readonly CustomerDbContext _context = context;
        public async Task CreateCustomer(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAll() => await _context.Customers.AsNoTracking().ToListAsync();

        public async Task<Customer?> GetByCpf(string cpf) => await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Cpf.Number == cpf);
    }
}
