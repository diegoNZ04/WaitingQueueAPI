using QueueSystem.Domain.Dtos;
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
        public async Task<List<ClientResponseDto>> GetAllClientsInQueueAsync()
        {
            var clients = await _queueRepository.GetAllClientsAsync();
            return clients.Select(c => new ClientResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email

            })
        }

        public Task<NextClientResponseDto> CallNextClientAsync()
        {
            throw new NotImplementedException();
        }

    }
}