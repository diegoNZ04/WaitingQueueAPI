using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Models;
using QueueSystem.Infra.Data;
using QueueSystem.Infra.Interfaces;

namespace QueueSystem.Infra.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _context;

        public ClientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<ClientModel>> GetClientByIdsAsync(List<int> clientIds)
        {
            return await _context.Clients.Where(c => clientIds.Contains(c.Id)).ToListAsync();
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

        public async Task<ClientModel> GetClientByIdAsync(int clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null)
            {
                throw new Exception($"Client with ID {clientId} not found.");
            }
            return client;
        }

        public async Task UpdateClientAsync(ClientModel client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}