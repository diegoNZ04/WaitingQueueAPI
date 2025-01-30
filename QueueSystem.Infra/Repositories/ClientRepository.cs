using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Entities;
using QueueSystem.Domain.Entities.Interfaces;
using QueueSystem.Infra.Data;

namespace QueueSystem.Infra.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationContext context) : base(context) { }

        public async Task<List<Client>> GetByQueueIdAsync(int queueId)
        {
            return await _context.Clients
                .Where(c => c.QueueId == queueId)
                .OrderBy(c => c.Priority)
                .ThenBy(c => c.Status)
                .ToListAsync();
        }
    }
}