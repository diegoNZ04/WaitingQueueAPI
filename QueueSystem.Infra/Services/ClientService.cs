using QueueSystem.Domain.Dtos;
using QueueSystem.Domain.Enums;
using QueueSystem.Domain.Models;
using QueueSystem.Infra.Interfaces;

namespace QueueSystem.Infra.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IQueueRepository _queueRepository;

        public ClientService(IClientRepository clientRepository, IQueueRepository queueRepository)
        {
            _clientRepository = clientRepository;
            _queueRepository = queueRepository;
        }

        public async Task<ClientModel> AddClientToQueueAsync(int queueId, ClientDto clientDto)
        {
            var queue = await _queueRepository.GetQueueByIdAsync(queueId);

            if (queue == null)
            {
                throw new Exception("Queue not found.");
            }

            var client = new ClientModel
            {
                Name = clientDto.Name,
                Category = clientDto.Category,
                Priority = clientDto.Priority,
                Status = ClientStatus.pending
            };

            await _clientRepository.AddClientToQueueAsync(queue, client);
            queue.Clients.Add(client);
            await _queueRepository.UpdateQueueAsync(queue);

            return client;
        }

        public async Task<int?> GetClientPositionAsync(int queueId, int clientId)
        {
            var queue = await _queueRepository.GetQueueByIdAsync(queueId);

            if (queue == null)
                throw new Exception("Queue not found.");

            var clientIds = queue.Clients.Select(c => c.Id).ToList();
            var clients = await _clientRepository.GetClientByIdsAsync(clientIds);
            var orderedClients = clients
                .OrderBy(c => c.Priority)
                .ThenBy(c => queue.Clients.ToList().IndexOf(c))
                .ToList();

            var position = orderedClients.FindIndex(c => c.Id == clientId);

            if (position == -1)
                throw new Exception("Client not found in the queue.");

            return position + 1;
        }

        public async Task RemoveClientFromQueueAsync(int queueId, int clientId)
        {
            var queue = await _queueRepository.GetQueueByIdAsync(queueId);

            if (queue == null)
            {
                throw new Exception("Queue not found.");
            }

            if (!queue.Clients.Any(c => c.Id == clientId))
            {
                throw new Exception("Client not found in the queue.");
            }

            var clientToRemove = queue.Clients.FirstOrDefault(c => c.Id == clientId);
            if (clientToRemove != null)
            {
                queue.Clients.Remove(clientToRemove);
            }

            await _queueRepository.UpdateQueueAsync(queue);

            var client = await _clientRepository.GetClientByIdAsync(clientId);

            if (client != null)
            {
                client.Status = ClientStatus.dropout;
                await _clientRepository.UpdateClientAsync(client);
            }
        }
    }
}