namespace QueueSystem.Domain.Entities.Interfaces
{
    public interface IQueueRepository : IGenericRepository<Queue>
    {
        Task<List<Client>> GetClientsInQueueAsync(int queueId);
    }
}