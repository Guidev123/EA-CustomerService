using CustomerService.Application;
using CustomerService.Application.BackgroundServices;
using CustomerService.Infrastructure;
using CustomerService.Infrastructure.MessageBus.Configuration;

namespace CustomerService.API.Middlewares
{
    public static class ApplicationMiddlewares
    {
        public static void AddMiddlewares(this WebApplicationBuilder builder)
        {
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddHostedService<CustomerBackgroundService>();
            builder.Services.Configure<BusSettingsConfiguration>(builder.Configuration.GetSection(nameof(BusSettingsConfiguration)));
        }
    }
}
