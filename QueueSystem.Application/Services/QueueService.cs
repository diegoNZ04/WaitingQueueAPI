using QueueSystem.Application.Dtos;
using QueueSystem.Domain.Entities;
using QueueSystem.Domain.Entities.Interfaces;
using QueueSystem.Infra.Services.Interfaces;

namespace QueueSystem.Infra.Services
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IBackgroundRepository _backgroundRepository;
        public QueueService(
            IQueueRepository queueRepository,
            IClientRepository clientRepository,
            IBackgroundRepository backgroundRepository)
        {
            _queueRepository = queueRepository;
            _clientRepository = clientRepository;
            _backgroundRepository = backgroundRepository;
        }
        public async Task<QueueResponse> CreateQueueAsync(string category, string description)
        {
            var existingQueue = await _queueRepository.GetByCategoryAsync(category);

            if (existingQueue != null)
                throw new Exception("Já existe uma fila com essa categoria");

            var queue = new Queue
            {
                Category = category,
                Description = description
            };

            await _queueRepository.AddAsync(queue);

            return new QueueResponse
            {
                Id = queue.Id,
                Category = queue.Category,
                Description = queue.Category,
                CreatedAt = queue.CreatedAt
            };
        }
        public async Task<CallNextClientResponse> CallNextClientAsync(int queueId)
        {
            var queue = await _queueRepository.GetByIdAsync(queueId);

            var nextClient = queue.Clients.Peek();

            if (nextClient == null)
                throw new Exception("Não há próximo cliente.");

            nextClient.Status = "Atendido";

            queue.Clients.Dequeue();

            await _queueRepository.UpdateAsync(queue);

            var background = new Background
            {
                ClientId = nextClient.Id,
                CalledAt = DateTime.UtcNow
            };
            await _backgroundRepository.AddAsync(background);

            return new CallNextClientResponse
            {
                Called = new ClientCalledResponse
                {
                    Id = nextClient.Id,
                    Name = nextClient.Name
                },
                Position = 1
            };

        }

        public async Task<List<ClientResponse>> ListAllClientsInQueueAsync(int queueId)
        {
            var clients = await _queueRepository.GetClientsInQueueAsync(queueId);

            return clients.Select((client, index) => new ClientResponse
            {
                Id = client.Id,
                Name = client.Name,
                Position = index + 1,
                Priority = client.Priority
            }).ToList();
        }
    }
}