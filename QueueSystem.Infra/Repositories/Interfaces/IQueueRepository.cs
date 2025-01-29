

using QueueSystem.Domain.Models;

namespace QueueSystem.Infra.Repositories.Interfaces
{
    public interface IQueueRepository
    {
        Task<QueueModel> GetByIdAsync(int id);
        Task<List<QueueModel>> GetAllAsync();
        Task AddAsync(QueueModel queue);
        Task UpdateAsync(QueueModel queue);
        Task DeleteAsync(Guid id);
        Task<List<ClientModel>> GetClientsInQueueAsync(int queueId);
    }
}