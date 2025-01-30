namespace QueueSystem.Domain.Entities.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<List<Client>> GetByQueueIdAsync(int queueId);
    }
}