using Microsoft.EntityFrameworkCore;
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

        public async Task<QueueModel> GetQueueByIdAsync(int queueId)
        {
            var queue = await _context.Queues.Include(q => q.Clients).FirstOrDefaultAsync(q => q.Id == queueId) ?? throw new InvalidOperationException($"Queue with ID {queueId} not found.");
            return queue;
        }

        public async Task AddClientToQueueAsync(QueueModel queue, ClientModel client)
        {
            queue.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveClientFromQueueAsync(QueueModel queue, ClientModel client)
        {
            queue.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

    }
}