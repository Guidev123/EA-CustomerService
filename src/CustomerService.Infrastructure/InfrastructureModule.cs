﻿using CustomerService.Domain.Repositories;
using CustomerService.Infrastructure.Persistence;
using CustomerService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Infrastructure
{
    public static class InfrastructureModule
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDbContext(configuration);
            services.AddRepositories();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}
