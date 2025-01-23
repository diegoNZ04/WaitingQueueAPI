using QueueSystem.Domain.Dtos;
using QueueSystem.Domain.Models;

namespace QueueSystem.Infra.Interfaces
{
    public interface IClientService
    {
        public Task<ClientModel> AddClientToQueueAsync(int queueId, ClientDto clienteDto);

        public Task<int?> GetClientPositionAsync(int queueId, int clientId);

        public Task RemoveClientFromQueueAsync(int queueId, int clientId);
    }
}