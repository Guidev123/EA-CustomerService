using CustomerService.Application;
using CustomerService.Infrastructure;

namespace CustomerService.API.Middlewares
{
    public static class ApplicationMiddlewares
    {
        public static void AddMiddlewares(this WebApplicationBuilder builder)
        {
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);
        }
    }
}
