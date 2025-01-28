using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Enums;
using QueueSystem.Domain.Models;
using QueueSystem.Infra.Data;
using QueueSystem.Infra.Interfaces;

namespace QueueSystem.Infra.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        private readonly ApplicationContext _context;

        public QueueRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<ClientModel>> GetAllClientsAsync()
        {
            return await _context.Clients
                .Where(c => c.Status == ClientStatus.pending)
                .OrderBy(c => c.Priority)
                .ThenBy(c => c.Id)
                .ToListAsync();
        }

        public async Task<QueueModel> GetQueueByIdAsync(int queueId)
        {
            var queue = await _context.Queues.FirstOrDefaultAsync(q => q.Id == queueId);
            if (queue == null)
            {
                throw new InvalidOperationException($"Queue with ID '{queueId}' not found.");
            }
            return queue;
        }

        public async Task UpdateQueueAsync(QueueModel queue)
        {
            _context.Queues.Update(queue);
            await _context.SaveChangesAsync();
        }

        public async Task<ClientModel> GetNextClientAsync()
        {
            var nextClient = await _context.Clients
                .Where(c => c.Status == ClientStatus.pending)
                .OrderBy(c => c.Priority)
                .ThenBy(c => c.Id)
                .FirstOrDefaultAsync();

            if (nextClient == null)
            {
                return null;
            }

            return nextClient;
        }
    }
}