using CustomerService.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CustomerService.Infrastructure.Services
{
    public sealed class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
    {
        private readonly ClaimsPrincipal _claims = httpContextAccessor.HttpContext!.User;

        public Task<Guid?> GetUserIdAsync()
        {
            var userIdClaim = _claims?.FindFirst("sub")?.Value ?? _claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdClaim, out var userId))
                return Task.FromResult<Guid?>(userId);

            return Task.FromResult<Guid?>(null);
        }
    }
}
