using QueueSystem.Domain.Dtos;

namespace QueueSystem.Infra.Interfaces
{
    public interface IQueueService
    {
        public Task<List<ClientResponseDto>> GetAllClientsInQueueAsync();

        public Task<NextClientResponseDto> CallNextClientAsync();
    }
}