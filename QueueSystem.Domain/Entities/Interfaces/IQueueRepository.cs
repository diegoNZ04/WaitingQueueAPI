namespace QueueSystem.Domain.Entities.Interfaces
{
    public interface IQueueRepository
    {
        Task<Queue> GetByIdAsync(int id);
        Task<List<Queue>> GetAllAsync();
        Task AddAsync(Queue queue);
        Task UpdateAsync(Queue queue);
        Task DeleteAsync(Guid id);
        Task<List<Client>> GetClientsInQueueAsync(int queueId);
    }
}