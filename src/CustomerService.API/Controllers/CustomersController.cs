using CustomerService.Application.Commands.AddAddress;
using CustomerService.Application.Services;
using CustomerService.Domain.Entities;
using CustomerService.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Domain.Mediator;
using SharedLib.Domain.Responses;

namespace CustomerService.API.Controllers
{
    [Route("api/v1/customers")]
    public class CustomersController(IMediatorHandler mediator,
                                     IUserService user,
                                     ICustomerRepository customerRepository) : MainController
    {
        private readonly IMediatorHandler _mediator = mediator;
        private readonly IUserService _user = user;
        private readonly ICustomerRepository _customerRepository = customerRepository;

        [HttpPost]
        public async Task<IResult> AddAddressAsync(AddAddressCommand command) => 
            CustomResponse(await _mediator.SendCommand(command));

        [HttpGet("address")]
        public async Task<IResult> GetAddressAsync()
        {
            var userId = await _user.GetUserIdAsync();
            if (!userId.HasValue || userId is null)
                return TypedResults.BadRequest();

            var address = await _customerRepository.GetAddressAsync(userId.Value);
            return address is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(new Response<Address>(address, 200));
        }
    }
}
