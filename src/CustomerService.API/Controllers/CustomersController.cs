using CustomerService.Application.Commands.DeleteCustomer;
using EA.CommonLib.Controllers;
using EA.CommonLib.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.API.Controllers
{
    [Route("api/v1/customers")]
    public class CustomersController(IMediator mediator) : MainController
    {
        private readonly IMediator _mediator = mediator;
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            return result.IsValid ? NoContent() : BadRequest(new ResponseResult
            {
                Title = "Error",
                Status = 400,
                Errors = new ResponseErrorMessages { Messages = result.Errors.Select(x => x.ErrorMessage).ToList() }
            });
        }
    }
}
