using QueueSystem.Domain.Dtos;
using QueueSystem.Domain.Models;
using QueueSystem.Infra.Repositories.Interfaces;
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
        public async Task<CallNextClientResponse> CallNextClientAsync(int queueId)
        {
            var clientsInQueue = await _queueRepository.GetClientsInQueueAsync(queueId);

            var nextClient = clientsInQueue
                .OrderBy(c => c.Priority == "Prioritário" ? 0 : 1)
                .ThenBy(c => c.Status == "Em Espera" ? 0 : 1)
                .FirstOrDefault();

            if (nextClient == null)
                return null;

            nextClient.Status = "Atendido";
            await _clientRepository.UpdateAsync(nextClient);

            var background = new BackgroundModel
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