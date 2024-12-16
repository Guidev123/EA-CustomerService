using CustomerService.Application.Commands.AddAddress;
using EA.CommonLib.Controllers;
using EA.CommonLib.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
