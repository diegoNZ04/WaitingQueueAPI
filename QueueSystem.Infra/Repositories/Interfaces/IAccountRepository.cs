
using QueueSystem.Domain.Models;

namespace QueueSystem.Infra.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task AddAsync(UserModel user);

        Task<UserModel> GetByEmailAsync(string email);
    }
}