

using QueueSystem.Domain.Models;

namespace QueueSystem.Infra.Interfaces
{
    public interface IQueueRepository
    {
        public Task<QueueModel> GetQueueByIdAsync(int queueId);

        public Task UpdateQueueAsync(QueueModel queue);
    }
}