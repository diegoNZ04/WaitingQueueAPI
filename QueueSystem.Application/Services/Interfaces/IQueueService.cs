

using QueueSystem.Application.Dtos;

namespace QueueSystem.Infra.Services.Interfaces
{
    public interface IQueueService
    {
        Task<List<ClientResponse>> ListAllClientsInQueueAsync(int queueId);
        Task<CallNextClientResponse> CallNextClientAsync(int queueId);
    }
}