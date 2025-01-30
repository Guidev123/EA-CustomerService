using CustomerService.Application.Commands.AddAddress;
using CustomerService.Application.Commands.Create;
using CustomerService.Application.Commands.Delete;
using EA.IntegrationEvents.Integration;
using EA.IntegrationEvents.Integration.DeletedUser;
using EA.IntegrationEvents.Integration.ReceivedAddress;
using EA.IntegrationEvents.Integration.RegisteredUser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedLib.Domain.Mediator;
using SharedLib.Domain.Responses;
using SharedLib.MessageBus;

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
            _bus.RespondAsync<DeletedUserIntegrationEvent, ResponseMessage>(DeleteCustomer);
            _bus.SubscribeAsync<ReceivedAddressIntegrationEvent>("ReceivedAddressIntegrationEvent", AddAddress);
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

            return new ResponseMessage(success.Data!.ValidationResult);
        }

        private async Task<ResponseMessage> DeleteCustomer(DeletedUserIntegrationEvent message)
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

        private async Task<ResponseMessage> AddAddress(ReceivedAddressIntegrationEvent message)
        {
            var clientCommand = new AddAddressCommand(message.Street, message.Number, message.AdditionalInfo,
                                message.Neighborhood, message.ZipCode, message.State, message.City);

            Response<AddAddressCommand> success;
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                success = await mediator.SendCommand(clientCommand);
            }

            return new ResponseMessage(success.Data!.ValidationResult!);
        }
    }
}
