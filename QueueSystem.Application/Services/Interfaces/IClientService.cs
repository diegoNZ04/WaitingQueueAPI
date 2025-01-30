

using QueueSystem.Domain.Entities;

namespace QueueSystem.Infra.Services.Interfaces
{
    public interface IClientService
    {
        public Task<Client> RegisterClientAsync(string name, string category, string priority, int queueId);
        public Task<int> ConsultPositionAsync(int clientId);
        public Task UnsubscribeAsync(int clientId);
    }
}