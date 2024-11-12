using CustomerService.Application.Commands.CreateCustomer;
using CustomerService.Application.IntegrationEvents;
using EA.CommonLib.Mediator;
using EA.CommonLib.MessageBus;
using EA.CommonLib.MessageBus.Integration;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomerService.Application.BackgroundServices
{
    public class CustomerBackgroundService : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CustomerBackgroundService(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponse()
        {
            _bus.RespondAsync<RegisteredUserIntegrationEvent, ResponseMessage>(async request =>
                await RegisterCustomer(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponse();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponse();
        }

        private async Task<ResponseMessage> RegisterCustomer(RegisteredUserIntegrationEvent message)
        {
            var clientCommand = new CreateCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult success;
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                success = await mediator.SendCommand(clientCommand);
            }
            return new ResponseMessage(success);
        }
    }
}
