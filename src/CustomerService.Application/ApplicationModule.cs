using CustomerService.Application.Commands.RegisterCustomer;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicationModule).Assembly));
            services.AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, RegisterCustomerHandler>();
        }
    }
}
