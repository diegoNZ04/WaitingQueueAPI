using QueueSystem.Domain.Models;

namespace QueueSystem.Infra.Interfaces
{
    public interface IQueueRepository
    {
        public Task<QueueModel> GetQueueByIdAsync(int queueId);

        public Task AddClientToQueueAsync(QueueModel queue, ClientModel client);

        public Task RemoveClientFromQueueAsync(QueueModel queue, ClientModel client);
    }
}