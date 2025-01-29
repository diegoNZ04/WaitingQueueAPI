using QueueSystem.Domain.Models;
using QueueSystem.Infra.Data;
using QueueSystem.Infra.Repositories.Interfaces;

namespace QueueSystem.Infra.Repositories
{
    public class BackgroundRepository : IBackgroundRepository
    {
        private readonly ApplicationContext _context;
        public BackgroundRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddAsync(BackgroundModel background)
        {
            await _context.Backgrounds.AddAsync(background);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var background = await _context.Backgrounds.FindAsync(id);
            if (background != null)
            {
                _context.Backgrounds.Remove(background);
                await _context.SaveChangesAsync();
            }
        }

        public Task UpdateAsync(BackgroundModel background)
        {
            throw new NotImplementedException();
        }
    }
}