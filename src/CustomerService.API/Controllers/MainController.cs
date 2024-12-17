using EA.CommonLib.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.API.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        public IResult CustomResponse<T>(Response<T> response) => response.Code switch
        {
            200 => TypedResults.Ok(response),
            400 => TypedResults.BadRequest(response),
            201 => TypedResults.Created(string.Empty, response),
            204 => TypedResults.NoContent(),
            _ => TypedResults.NotFound()
        };
    }
}
