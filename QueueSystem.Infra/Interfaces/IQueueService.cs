using QueueSystem.Domain.Dtos;
using QueueSystem.Domain.Models;

namespace QueueSystem.Infra.Interfaces
{
    public interface IQueueService
    {
        public Task<ClientModel> AddClientToQueueAsync(int queueId, ClientDto clientDto);

        public Task<int?> GetClientPositionAsync(int queueId, int clientId);

        public Task RemoveClientFromQueueAsync(int queueId, int clientId);
    }
}