using QueueSystem.Domain.Dtos;
using QueueSystem.Domain.Models;
using QueueSystem.Infra.Interfaces;

namespace QueueSystem.Infra.Services
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _queueRepository;

        public QueueService(IQueueRepository queueRepository)
        {
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
                Priority = clientDto.Priority
            };

            await _queueRepository.AddClientToQueueAsync(queue, client);
            return client;
        }

        public async Task<int?> GetClientPositionAsync(int queueId, int clientId)
        {
            var queue = await _queueRepository.GetQueueByIdAsync(queueId);

            if (queue == null)
                return null;

            var clientsOrdered = queue.Clients
                .OrderBy(c => c.Priority)
                .ThenBy(c => c.Id)
                .ToList();

            var clientIndex = clientsOrdered.FindIndex(c => c.Id == clientId);
            return clientIndex >= 0 ? clientIndex + 1 : (int?)null;
        }

        public async Task RemoveClientFromQueueAsync(int queueId, int clientId)
        {
            var queue = await _queueRepository.GetQueueByIdAsync(queueId);

            if (queue == null)
            {
                throw new Exception("Queue not found.");
            }

            var client = queue.Clients.FirstOrDefault(c => c.Id == clientId);

            if (client == null)
            {
                throw new Exception("Client not found in the queue.");
            }

            await _queueRepository.RemoveClientFromQueueAsync(queue, client);
        }
    }
}