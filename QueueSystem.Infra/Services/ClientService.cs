

using QueueSystem.Domain.Models;
using QueueSystem.Infra.Repositories.Interfaces;
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

        public async Task<ClientModel> RegisterClientAsync(string name, string category, string priority, int queueId)
        {
            var client = new ClientModel
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
                throw new Exception("Client not found");

            var clientsInQueue = await _clientRepository.GetByQueueIdAsync(client.QueueId);

            return clientsInQueue.FindIndex(c => c.Id == clientId) + 1;
        }

        public async Task UnsubscribeAsync(int clientId)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);

            if (client != null)
            {
                client.Status = "Desistente";
                await _clientRepository.UpdateAsync(client);
            }
        }


    }
}