using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Models;
using QueueSystem.Infra.Data;
using QueueSystem.Infra.Repositories.Interfaces;

namespace QueueSystem.Infra.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        private readonly ApplicationContext _context;

        public QueueRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(QueueModel queue)
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

        public async Task<List<QueueModel>> GetAllAsync()
        {
            return await _context.Queues.ToListAsync();
        }

        public async Task<QueueModel> GetByIdAsync(int id)
        {
            return await _context.Queues
                .Include(q => q.Clients)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<ClientModel>> GetClientsInQueueAsync(int queueId)
        {
            return await _context.Clients
                .Where(c => c.QueueId == queueId)
                .OrderBy(c => c.Priority)
                .ThenBy(c => c.Status)
                .ToListAsync();
        }

        public async Task UpdateAsync(QueueModel queue)
        {
            _context.Queues.Update(queue);
            await _context.SaveChangesAsync();
        }
    }
}