using CustomerService.Application.BackgroundServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLib.Domain.Mediator;
using SharedLib.MessageBus;

namespace CustomerService.Application
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediator();
            services.AddMessageBusConfiguration(configuration);
        }

        public static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicationModule).Assembly));
            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }

        public static void AddMessageBusConfiguration(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
            services.AddHostedService<CustomerBackgroundService>();
        }
    }
}
