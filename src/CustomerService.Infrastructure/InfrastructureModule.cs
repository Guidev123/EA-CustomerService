using CustomerService.Application.Services;
using CustomerService.Domain.Repositories;
using CustomerService.Infrastructure.Persistence;
using CustomerService.Infrastructure.Persistence.Repositories;
using CustomerService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerService.Infrastructure
{
    public static class InfrastructureModule
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDbContext(configuration);
            services.AddRepositories();
            services.AddServices();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}
