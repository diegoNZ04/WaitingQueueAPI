using QueueSystem.Domain.Models;

namespace QueueSystem.Infra.Repositories.Interfaces
{
    public interface IBackgroundRepository
    {
        Task AddAsync(BackgroundModel background);
        Task UpdateAsync(BackgroundModel background);
        Task DeleteAsync(int id);
    }
}