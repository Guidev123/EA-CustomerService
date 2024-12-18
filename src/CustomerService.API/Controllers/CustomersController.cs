using CustomerService.Application.Commands.AddAddress;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Domain.Mediator;

namespace CustomerService.API.Controllers
{
    [Route("api/v1/customers")]
    public class CustomersController(IMediatorHandler mediator) : MainController
    {
        private readonly IMediatorHandler _mediator = mediator;

        [HttpPost]
        public async Task<IResult> AddAddress(AddAddressCommand command) => 
            CustomResponse(await _mediator.SendCommand(command));
    }
}
