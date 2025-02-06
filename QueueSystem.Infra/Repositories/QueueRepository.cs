using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Entities;
using QueueSystem.Domain.Entities.Interfaces;
using QueueSystem.Infra.Data;

namespace QueueSystem.Infra.Repositories
{
    public class QueueRepository : GenericRepository<Queue>, IQueueRepository
    {
        public QueueRepository(ApplicationContext context) : base(context) { }


        public async Task<List<Client>> GetClientsInQueueAsync(int queueId)
        {
            return await _context.Clients
                .Where(c => c.QueueId == queueId)
                .OrderBy(c => c.Priority)
                .ThenBy(c => c.Status)
                .ToListAsync();
        }

        public async Task<Queue> GetByCategoryAsync(string category)
        {
            return await _context.Queues.FindAsync(category);
        }

    }
}