using QueueSystem.Domain.Models;

namespace QueueSystem.Infra.Interfaces
{
    public interface IClientRepository
    {
        public Task<List<ClientModel>> GetClientByIdsAsync(List<int> clientIds);

        public Task<ClientModel> GetClientByIdAsync(int clientId);

        public Task AddClientToQueueAsync(QueueModel queue, ClientModel client);

        public Task UpdateClientAsync(ClientModel client);

        public Task RemoveClientFromQueueAsync(QueueModel queue, ClientModel client);
    }
}