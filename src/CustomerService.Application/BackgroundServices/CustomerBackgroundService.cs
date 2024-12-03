using CustomerService.Application.Commands.CreateCustomer;
using CustomerService.Application.Commands.DeleteCustomer;
using EA.CommonLib.Mediator;
using EA.CommonLib.MessageBus;
using EA.CommonLib.MessageBus.Integration;
using EA.CommonLib.MessageBus.Integration.DeleteCustomer;
using EA.CommonLib.MessageBus.Integration.RegisteredCustomer;
using EA.CommonLib.Responses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomerService.Application.BackgroundServices
{
    public class CustomerBackgroundService(IServiceProvider serviceProvider, IMessageBus bus)
               : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly IMessageBus _bus = bus;

        private void SetResponse()
        {
            _bus.RespondAsync<RegisteredUserIntegrationEvent, ResponseMessage>(RegisterCustomer);
            _bus.RespondAsync<DeleteCustomerIntegrationEvent, ResponseMessage>(DeleteCustomer);
            _bus.AdvancedBus.Connected += OnConnect!;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponse();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e) => SetResponse();

        private async Task<ResponseMessage> RegisterCustomer(RegisteredUserIntegrationEvent message)
        {
            var clientCommand = new CreateCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);
            Response<CreateCustomerCommand> success;
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                success = await mediator.SendCommand(clientCommand);
            }

            return new ResponseMessage(success.Data!.ValidationResult!);
        }

        private async Task<ResponseMessage> DeleteCustomer(DeleteCustomerIntegrationEvent message)
        {
            var clientCommand = new DeleteCustomerCommand(message.Id);
            Response<DeleteCustomerCommand> success;
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                success = await mediator.SendCommand(clientCommand);
            }

            return new ResponseMessage(success.Data!.ValidationResult!);
        }
    }
}
