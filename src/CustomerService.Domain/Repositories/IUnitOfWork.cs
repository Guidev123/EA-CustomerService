namespace CustomerService.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
