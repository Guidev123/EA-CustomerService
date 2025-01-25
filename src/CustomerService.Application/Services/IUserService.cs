namespace CustomerService.Application.Services
{
    public interface IUserService
    {
        Task<Guid?> GetUserIdAsync();
    }
}
