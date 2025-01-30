using QueueSystem.Domain.Entities;
using QueueSystem.Domain.Entities.Interfaces;
using QueueSystem.Infra.Services.Interfaces;

namespace QueueSystem.Infra.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> RegisterClientAsync(string name, string category, string priority, int queueId)
        {
            var client = new Client
            {
                Name = name,
                Category = category,
                Priority = priority,
                QueueId = queueId
            };

            await _clientRepository.AddAsync(client);
            return client;
        }

        public async Task<int> ConsultPositionAsync(int clientId)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);

            if (client == null)
                throw new Exception("Client não encontrado");

            var clientsInQueue = await _clientRepository.GetByQueueIdAsync(client.QueueId);

            return clientsInQueue.FindIndex(c => c.Id == clientId) + 1;
        }

        public async Task UnsubscribeAsync(int clientId)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);

            if (client != null)
            {
                await _clientRepository.DeleteAsync(client.Id);
            }

        }


    }
}