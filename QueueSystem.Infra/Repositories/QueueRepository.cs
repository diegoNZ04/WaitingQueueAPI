using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Entities;
using QueueSystem.Domain.Entities.Interfaces;
using QueueSystem.Infra.Data;

namespace QueueSystem.Infra.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        private readonly ApplicationContext _context;

        public QueueRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Queue queue)
        {
            await _context.Queues.AddAsync(queue);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var queue = await _context.Queues.FindAsync(id);

            if (queue != null)
            {
                _context.Queues.Remove(queue);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Queue>> GetAllAsync()
        {
            return await _context.Queues.ToListAsync();
        }

        public async Task<Queue> GetByIdAsync(int id)
        {
            return await _context.Queues
                .Include(q => q.Clients)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<Client>> GetClientsInQueueAsync(int queueId)
        {
            return await _context.Clients
                .Where(c => c.QueueId == queueId)
                .OrderBy(c => c.Priority)
                .ThenBy(c => c.Status)
                .ToListAsync();
        }

        public async Task UpdateAsync(Queue queue)
        {
            _context.Queues.Update(queue);
            await _context.SaveChangesAsync();
        }
    }
}