namespace QueueSystem.Domain.Entities.Interfaces
{
    public interface IAccountRepository
    {
        Task AddAsync(User user);

        Task<User> GetByEmailAsync(string email);
    }
}