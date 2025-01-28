using QueueSystem.Domain.Models;

namespace QueueSystem.Infra.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<ClientModel> GetByIdAsync(int id);
        Task<List<ClientModel>> GetByQueueIdAsync(int queueId);
        Task AddAsync(ClientModel client);
        Task UpdateAsync(ClientModel client);
        Task DeleteAsync(int id);
    }
}