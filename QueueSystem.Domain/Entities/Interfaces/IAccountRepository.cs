namespace QueueSystem.Domain.Entities.Interfaces
{
    public interface IAccountRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}